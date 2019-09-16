using UnityEngine;
using MalbersAnimations;
using MalbersAnimations.Utilities;
using System.Collections.Generic;

namespace MalbersAnimations.Controller
{
    /// <summary>
    /// This will controll all Animals Motion it is more Modular
    /// Version 2.0
    /// </summary>
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody))]
    public partial class MAnimal : MonoBehaviour, IAnimatorListener, ICharacterMove, IGravity, IMDamage//, IMHitLayer// ,IInteractable
    {
        //This was left in blank Intentionally

        //Animal Variables: All variables
        //Animal Movement:  All Locomotion Logic
        //Animal CallBacks: All public methods and behaviors that it can be called outside the script

        #region Editor Show 
        [HideInInspector] public bool showPivots = true;
        [HideInInspector] public Color ShowpivotColor = Color.blue;
        [HideInInspector] public bool showStates = true;
        //[HideInInspector] public bool showModes = true;
        //[HideInInspector] public bool show_General = true;
       // [HideInInspector] public int ModeIndexSelected;
        [HideInInspector] public bool ModeShowAbilities;
        [HideInInspector] public int Editor_Tabs1;
        [HideInInspector] public int Editor_Tabs2;
        #endregion

         
        void Reset()
        {
            MalbersTools.SetLayer(transform, 20);     //Set all the Childrens to Animal Layer   .
            gameObject.tag = "Animal";                //Set the Animal to Tag Animal
            AnimatorSpeed = 1;

            speedSets = new List<MSpeedSet>(1)
            {
                new MSpeedSet()
            {
                name = "Ground",
                    StartVerticalIndex = new Scriptables.IntReference(1),
                    TopIndex = new Scriptables.IntReference(2),
                    states =  new  List<StateID>(2) { MalbersTools.GetInstance<StateID>("Idle") , MalbersTools.GetInstance<StateID>("Locomotion")},
                    Speeds =  new  List<MSpeed>(3) { new MSpeed("Walk",0,4,4) , new MSpeed("Trot", 0, 4, 4), new MSpeed("Run", 0, 4, 4) }
            }
            };
        }


#if UNITY_EDITOR
        /// <summary> Debug Options </summary>
        void OnDrawGizmos()
        {
            if (!debugGizmos) return;

            float sc = transform.localScale.y;

            if (Application.isPlaying)
            {
                if (ActiveState != null) ActiveState.DebugState();


                Gizmos.color = Color.magenta;
                Gizmos.DrawWireSphere(Main_Pivot_Point, RayCastRadius * sc);

                if (MainRay)
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawWireSphere(hit_Hip.point+AdditivePosition, RayCastRadius * sc);
                    Debug.DrawRay(hit_Hip.point + AdditivePosition, hit_Hip.normal * 0.2f, Color.blue * sc);
                }
                if (FrontRay)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireSphere(hit_Chest.point + AdditivePosition, RayCastRadius * sc);
                    Debug.DrawRay(hit_Chest.point + AdditivePosition, hit_Chest.normal * 0.2f, Color.red * sc);
                }

                Gizmos.color = Color.black;
                Gizmos.DrawSphere(_transform.position + DeltaPos, 0.02f * sc);

                //  Debug.DrawRay(transform.position, RB.velocity, Color.magenta );

                //float mag = Inertia.magnitude;

                //if (!Mathf.Approximately(mag, 0))
                //{
                //    UnityEditor.Handles.color = Color.magenta;
                //    Quaternion InertiaRot = Quaternion.LookRotation(RB.velocity);
                //    UnityEditor.Handles.ArrowHandleCap(10, transform.position, InertiaRot, RB.velocity.magnitude, EventType.Repaint);
                //}
            }

            if (showPivots)
            {
                for (int i = 0; i < pivots.Count; i++)
                {
                    if (pivots[i] != null)
                    {
                        Gizmos.color = ShowpivotColor;
                        Gizmos.DrawWireSphere(pivots[i].World(transform), 0.05f * sc);
                        Gizmos.DrawRay(pivots[i].World(transform), -transform.up * pivots[i].multiplier * sc);
                    }
                }
            }
        }
#endif
    }
}
