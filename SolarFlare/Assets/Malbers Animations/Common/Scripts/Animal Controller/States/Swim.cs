﻿using MalbersAnimations.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace MalbersAnimations.Controller
{
    /// <summary>Swim Logic</summary>
    public class Swim : State
    {
        [Header("Swim Paramenters")]
        /// <summary>Level of the Animal to be on the Water </summary>
        // public float WaterLine;
        //public float WaterWaitTime = 1;
        //protected float EnterWateTime = 0;
        public float AlignSmooth = 10;

        [Tooltip("When entering the water the Animal will sink for a while... Higher values, will return to the surface faster")]
        public float Bounce = 5;
        [Tooltip("If the Animal Enters the water time elapsed to try exiting the water")]
        public float TryExitTime = 1;
        protected float EnterWaterTime;
        [Tooltip("Means the Water does not change the shape. Less RayCasting")]
        public bool WaterIsStatic = true;
        public bool KeepInertia = true;
        public float Radius = 0.025f;

        //True: Pivot above the water, False: Pivot Below the water.
        public bool PivotAboveWater { get; set; }

        /// <summary>Has the animal found Water</summary>
        public bool IsInWater { get; set; }
        protected int WaterLayer;
        protected MPivots WaterPivot;
        protected Vector3 WaterNormal = Vector3.up;
        protected Vector3 Inertia;

        /// <summary>Water Collider used on the Sphere Cast</summary>
        protected Collider[] WaterCollider;
        /// <summary>Point Above the Animal to Look Down and Find the Water level and Water Normal</summary>
        // private Vector3 WaterUPAnimalPos;
        private Vector3 WaterPivot_Dist_from_Water;
        private Vector3 WaterUPPivot;
        private Vector3 UpImpulse;
        const float UpMult = 8;
        public Vector3 WaterPivotPoint
        {
            get { return WaterPivot.World(animal.transform); }// + animal.AdditivePosition; }
        }


        public override void StartState()
        {
            WaterLayer = LayerMask.GetMask("Water");

            WaterPivot = animal.pivots.Find(p => p.name.ToLower().Contains("water"));      //Find the Water Pivot
            if (WaterPivot == null) Debug.LogError("No Water Pivot Found.. please create a Water Pivot");

            WaterCollider = new Collider[1];
            IsInWater = false;
        }

        public override void Activate()
        {
            base.Activate();

            Inertia = Vector3.ProjectOnPlane(animal.DeltaPos, animal.UpVector);
            UpImpulse = MalbersTools.CleanUpVector(animal.DeltaPos, animal.Forward, animal.Right);   //Clean the Vector from Forward and Horizontal Influence    
            IgnoreLowerStates = true;                                                           //Ignore Falling, Idle and Locomotion while swimming 
                                                                                                //animal.InertiaPositionSpeed = Inertia; //THIS MOTHER F!#$ER was messing with the water entering

            animal.InertiaPositionSpeed = Vector3.zero; //THIS MOTHER F!#$ER was messing with the water entering
        }

        public override bool TryActivate()
        {
            CheckWater();

            if (IsInWater)
            {
                WaterUPPivot = WaterPivotPoint + animal.AdditivePosition + (animal.UpVector * UpMult);   //Set the Up Point

                var waterCol = WaterCollider[0];

                RaycastHit WaterHit;
                Ray WaterRay = new Ray(WaterUPPivot, animal.GravityDirection);

                if (waterCol.Raycast(WaterRay, out WaterHit, 100f))
                {
                    WaterNormal = WaterHit.normal;
                }

                 EnterWaterTime = Time.time;

                return true;
            }

            return false;
        }


        public void CheckWater()
        {
            int WaterFound = Physics.OverlapSphereNonAlloc(WaterPivotPoint, Radius * animal.ScaleFactor, WaterCollider, WaterLayer); //NOT WORKING ON CLONE SWIM
            // WaterCollider = Physics.OverlapSphere(WaterPivotPoint, Radius, WaterLayer, QueryTriggerInteraction.Collide); // When cloning Works
            IsInWater = WaterFound > 0;                 //Means the Water SphereOverlap Found Water
        }

        public override void TryExitState(float DeltaTime)
        {
            //if (MainTagHash == CurrentAnimTag)
            {
                if (Time.time - EnterWaterTime < TryExitTime) return; //do not try to exit if the animal just enter the water

                CheckWater();

                if (!IsInWater)
                {
                    AllowExit();
                }
            }
        }


        public override void OnStateMove(float deltatime)
        {
            //  Vector3 AnimalPos = animal.transform.position + animal.AdditivePosition;
            //WaterUPAnimalPos = AnimalPos + (animal.UpVector * UpMult);   //Set the Up Point
            WaterUPPivot = WaterPivotPoint + animal.AdditivePosition + (animal.UpVector * UpMult);   //Set the Up Point

            if (IsInWater)
            {
                if (KeepInertia)
                {
                    animal.AddInertia(ref Inertia, deltatime);
                }
                if (Bounce > 0)
                {
                    animal.AddInertia(ref UpImpulse, deltatime * Bounce);
                }


                var waterCol = WaterCollider[0];                                                //Get the Water Collider
                                                                                                // var WaterUPSurface = waterCol.ClosestPoint(WaterUPAnimalPos);

                if (!WaterIsStatic)                     //Means that the Water Changes shape
                {
                    RaycastHit WaterHit;
                    Ray WaterRay = new Ray(WaterUPPivot, animal.GravityDirection * UpMult);

                    if (waterCol.Raycast(WaterRay, out WaterHit, 100f)) WaterNormal = WaterHit.normal;        //RayCast to find the Water Normal
                }

                animal.AlignRotation(WaterNormal, deltatime, AlignSmooth);                                     //Aling the Animal to the Water 


                //Find the Water Level
                FindWaterLevel();

                //  Debug.Log(WaterPivot_Dist_from_Water);
                if (WaterPivot_Dist_from_Water.magnitude > 0.001f)
                {
                    animal.AdditivePosition += WaterPivot_Dist_from_Water * (deltatime * AlignSmooth);
                }


                //  animal.AlignPositionDistance(WaterPivot_Dist_from_Water,  deltatime, AlignSmooth);         //Align to Water Level

                //  float Animal_Dist_from_Water = (WaterUPSurface - AnimalPos).magnitude;// + WaterLine;
                // animal.AlignPositionInverse(Animal_Dist_from_Water + WaterLine, deltatime, AlignSmooth);         //Align to Water Level

            }
        }

        public void FindWaterLevel()
        {
            if (IsInWater)
            {
                var waterCol = WaterCollider[0];
                var PivotPointDistance = waterCol.ClosestPoint(WaterUPPivot);
                WaterPivot_Dist_from_Water = (PivotPointDistance - WaterPivotPoint);
                WaterPivot_Dist_from_Water = MalbersTools.CleanUpVector(WaterPivot_Dist_from_Water, animal.Forward, animal.Right);

                PivotAboveWater = Vector3.Dot(WaterPivot_Dist_from_Water, animal.UpVector) < 0;
            }
            else
            {
                PivotAboveWater = true;
            }
        }

        public override void ExitState()
        {
            base.ExitState();
            WaterUPPivot = Vector3.zero;
            WaterCollider = new Collider[1];
            IsInWater = false;
            EnterWaterTime = 0;
        }

#if UNITY_EDITOR

        public override void Reset()
        {
            ID = MalbersTools.GetInstance<StateID>("Swim");

            WaterCollider = new Collider[1];            //Set the Array to 1

            General = new AnimalModifier()
            {
                RootMotion = true,
                Grounded = false,
                Sprint = true,
                OrientToGround = false,
                CustomRotation = true,
                IgnoreLowerStates = true, //IMPORTANT
                AdditivePosition = true,
                Colliders = true,
                //AdditiveRotation = true,

                Gravity = false,
                modify = (modifier)(-1),
                //KeepInertia = true
            };
            // SpeedIndex = 0;
        }


        public override void DebugState()
        {
            if (Application.isPlaying)
            {
                if (IsInWater)
                {
                    var scale = animal.ScaleFactor;
                    Collider WaterCol = WaterCollider[0];

                    Gizmos.color = Color.blue;
                    Gizmos.DrawSphere(WaterPivotPoint, Radius * scale);
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(animal.transform.position, Radius * scale);

                    if (WaterCol)
                    {
                        Gizmos.color = Color.cyan;
                        Gizmos.DrawSphere(WaterCol.ClosestPoint(WaterUPPivot), Radius * scale);
                        Gizmos.DrawSphere(WaterUPPivot, Radius * scale);
                    }
                }
            }
        }
#endif
    }
}
