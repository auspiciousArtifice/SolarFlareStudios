﻿using MalbersAnimations.Events;
using MalbersAnimations.Scriptables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MalbersAnimations.Controller
{
    [System.Serializable] //DO NOT REMOVE!!!!!!!!!
    public class Mode
    {
        public bool PlayingMode { get; set; }
        /// <summary>Is this Mode Active?</summary>
        public bool active = true;
        /// <summary>Animation Tag Hash of the Mode</summary>
        public string AnimationTag;
        /// <summary>Animation Tag Hash of the Mode</summary>
        protected int ModeTagHash;
        /// <summary>Which Input Enables the Ability </summary>
        public string Input;
        /// <summary>ID of the Mode </summary>
        public ModeID ID;
        /// <summary>Modifier that can be used when the Mode is Enabled/Disabled or Interrupted</summary>
        public ModeModifier modifier;

        /// <summary>Elapsed time to be interrupted by another Mode If 0 then the Mode cannot be interrupted by any other mode </summary>
        public FloatReference CoolDown =new FloatReference( 0);
        protected float Current_CoolDown_Time;
        /// <summary>Global Properties for the Mode </summary>
        public ModeProperties GlobalProperties;

        /// <summary>Active Ability index</summary>
        [SerializeField]  private IntReference abilityIndex = new IntReference(-1);
        public IntReference DefaultIndex = new IntReference( -1);
        public IntEvent OnAbilityIndex = new IntEvent();
        public bool ResetToDefault = false;
        // public bool Customize = false;

        public string Name {
            get
            {
                if (ID) return ID.name;

                return string.Empty;
            }
        }

        /// <summary>List of Abilities </summary>
        public List<Ability> Abilities;

        ///// <summary>List of Abilities converted to Dictionary </summary>
        //public Dictionary<int,Ability> d_Abilities;

        public MAnimal animal { get; private set; }

        /// <summary> Current Selected Ability to Play on the Mode</summary>
        public Ability ActiveAbility { get; set; }

        /// <summary>Current Value of the Input if this mode was called  by an Input</summary>
        public bool InputValue { get; set; }

        /// <summary>Set everyting up when the Animal Script Start</summary>
        public virtual void AwakeMode(MAnimal animal)
        {
            this.animal = animal;                                   //Cache the Animal
            ModeTagHash = Animator.StringToHash(AnimationTag);      //Convert the Tag on a TagHash
 
            OnAbilityIndex.Invoke(AbilityIndex);
            Current_CoolDown_Time = - CoolDown; //First Time IMPORTANT
        }



        /// <summary>Exit the current mode an ability</summary> 
        public virtual void ExitMode()
        {
            animal.IsPlayingMode = PlayingMode = false;

            if (modifier != null) modifier.OnModeExit(this);

            OnExitInvoke();

            if (ResetToDefault) ResetAbilityIndex();        //Reset to the default

            ActiveAbility = null;                            //Reset to the default
        }

        /// <summary>Resets the Ability Index on the  animal to the default value</summary>
        public virtual void ResetAbilityIndex()
        {
            if (!animal.IsOnZone)
            {
                AbilityIndex = DefaultIndex;
            } 
        }

        public void ActivatebyInput(bool Input_Value)
        {
            if (!animal.enabled) return;

            if (InputValue != Input_Value)
            {
                InputValue = Input_Value;

                if (animal.InputMode == null && InputValue)
                {
                    animal.InputMode = this;
                }
                else if (animal.InputMode == this && !InputValue)
                {
                    animal.InputMode = null;
                }

                 if (animal.LockInput) return;            //Do no Activate if is sleep or disable or lock input is Enable;


                if (animal.debugModes) Debug.Log(" Mode: " + ID.name + " Input: " + Input_Value);

                if (InputValue)
                {
                    if (animal.debugModes) Debug.Log("Try Activate Mode: " + ID.name + " by INPUT");

                    if (CheckStatus(AbilityStatus.Toggle))     { animal.InputMode = null; }
                

                    TryActivate();
                }
                else
                {
                    if (PlayingMode)
                    {
                        if (CheckStatus(AbilityStatus.HoldByInput) && !InputValue)
                        {
                            if (animal.debugModes) Debug.Log("Exit Mode: " + ID.name + " by INPUT Up");
                            animal.Mode_Interrupt();
                        }
                    }
                }
            }
        }
        /// <summary>Activates an Ability from this mode using the AbilityIndex</summary>
        public virtual void TryActivate()
        {
            if (!active) return;                //If the mode is Disabled Ingnore

            //if (animal.debugModes) Debug.Log("Mode " + ID.name + " Try to Activate");

            if (Abilities == null || Abilities.Count == 0)
            {
                Debug.LogError("There's no Abilities on <b>" + Name + " Mode</b>, Please set a list of Abilities");
                return;
            } 

            if (CheckStatus(AbilityStatus.Toggle)) //if is set to Toggle then if is already playing this mode then stop it
            {
                InputValue = false;
                if (animal.InputMode == this) animal.InputMode = null;
             

                if (PlayingMode)
                {
                    if (animal.debugModes) Debug.Log("Mode <b>" + ID.name + "</b> Toggle Off");
                    animal.Mode_Interrupt();
                    return;
                }
            }
             
            var ActiveMode = animal.ActiveMode;

            if (animal.IsPlayingMode)              // Means the there's a Mode Playing
            {
                if (ActiveMode.CoolDown == 0)
                {
                    //if (animal.debugModes) Debug.Log("<color=red>Mode <b>" + ID.name + "</b> cannot be activated. CoolDown = 0, Animal is still playing a Mode</color>");
                    return;
                }

                if (Time.time - ActiveMode.Current_CoolDown_Time > ActiveMode.CoolDown)   //Exit the Mode After the cooldown has Passed
                {
                    ExitMode();
                    animal.Mode_Stop(); //This allows to Play a mode again
                  //  return;
                }
                return;
            }

            if (Time.time - Current_CoolDown_Time < CoolDown)
            {
              //  if (animal.debugModes) Debug.Log("<color=red>Mode <b>" + ID.name + "</b> cannot be activated. The Mode is still in cooldown Time</color>");
                return; //Do not Start another Mode Mode until the Cool Down has pass (even if there's no Active Mode);
            }

            var affect = GlobalProperties.affect;

            if (!AffectStates_Empty)
            {
                if (affect == AffectStates.Exclude && ContainState(animal.ActiveState.ID)      //If the new state is on the Exclude State
            || (affect == AffectStates.Inlcude && !ContainState(animal.ActiveState.ID)))    //OR If the new state is not on the Include State
                {
                    if (animal.debugModes) Debug.Log("<color=red>Mode <b>" + ID.name + "</b> cannot be activated. The Mode is Not Included or is Excluded from the current Animal State</color>");
                    return;
                }
            }

            if (modifier != null) modifier.OnModeEnter(this); //Check first if there's a modifier on Enter

            if (AbilityIndex == 0) return;        //Means that no Ability is Active

            try
            {
                if (AbilityIndex == -1)
                {
                    int randomIndex = Random.Range(0, Abilities.Count);
                    if (animal.debugModes) Debug.Log("Activate Random " + ID.name+". Random Ability: "+randomIndex);
                    Activate(Abilities[randomIndex].Index);
                }
                else
                {
                    Activate(AbilityIndex);
                }
            }
            catch
            {
                Debug.LogError("There's no Abilities on " + Name + " Mode, Please set a list of Abilities");
            }
        }


        /// <summary>Randomly Activates an Ability from this mode</summary>
        protected virtual void Activate(int AbilityIndex)
        {
            ActiveAbility = Abilities.Find(item => item.Index == AbilityIndex);

            if (ActiveAbility != null)
            {
                if (animal.debugModes) Debug.Log("Mode: '" + ID.name + "' with Ability: '" + ActiveAbility.Name + "' Activated");
                animal.ActiveMode = this;
                Current_CoolDown_Time = Time.time;                 //Save the Time the Mode was active
              //  if (animal.debugModes) Debug.Log("Current_CoolDown_Time: '" + Current_CoolDown_Time);
                animal.IsPlayingMode = PlayingMode = true;      //Set that the Animal is Playing a Mode
            }
        }



        /// <summary>
        /// Called by the Mode Behaviour on Entering the Animation State 
        /// Done this way to check for Modes that are on other Layers besides the Base Layer </summary>
        public void AnimationTagEnter(int animatorTagHash)
        {
             if (animatorTagHash != ModeTagHash) return; // if we are not on this Tag then Skip the code

            if (ActiveAbility != null)
            {
                animal.IsPlayingMode =  PlayingMode = true;
               
                OnEnterInvoke();                                        //Invoke the ON ENTER Event

               // ActiveAbility.IsPlayingAbility = true; //Set to the Ability that is playing the animation(s)

                AbilityStatus AMode = ActiveAbility.OverrideProp ? ActiveAbility.OverrideProperties.Status : GlobalProperties.Status; //Check if the Current Ability overrides the global properties
                float HoldByTime = ActiveAbility.OverrideProp ? ActiveAbility.OverrideProperties.HoldByTime : GlobalProperties.HoldByTime;

                int IntID = Int_ID.Loop;    //That means the Ability is Loopable

                if (AMode == AbilityStatus.PlayOneTime)
                {
                    IntID = Int_ID.OneTime;                //That means the Ability is OneTime 
                    if (animal.debugModes) Debug.Log("AnimationTag Enter Mode: '" + ID.name + "' with Ability: '" + ActiveAbility.Name +"' PlayOneTime '");
                }
                else if (AMode == AbilityStatus.HoldByTime)
                {
                    animal.StartCoroutine(Ability_By_Time(HoldByTime));
                    if (animal.debugModes) Debug.Log("AnimationTag Enter Mode: '" + ID.name + "' with Ability: '" + ActiveAbility.Name + "' HoldByTime '");
                }
                //else if (AMode == AbilityStatus.HoldByInput)
                //{
                //    IntID = Int_ID.OneTime;                //That means the Ability is OneTime 
                //    if (animal.debugModes) Debug.Log("AnimationTag Enter Mode: '" + ID.name + "' with Ability: '" + ActiveAbility.Name + "' Hold By Input '");
                //}

                animal.SetIntID(IntID);
            }
        }

        /// <summary>
        /// Called by the Mode Behaviour on Exiting the  Animation State 
        /// Done this way to check for Modes that are on other Layers besides the base one
        /// </summary>
        public void AnimationTagExit(int AbilityIndex)
        {
            if (animal.ActiveMode == this && AbilityIndex == ActiveAbility.Index)               //Means that we just exit This Mode mode //Do not exit if we are already on another index
            {
                if (animal.debugModes) Debug.Log("AnimationTagExit Mode: '" + ID.name + "' with Ability: '" + ActiveAbility.Name);

                ExitMode();
                animal.Mode_Stop();
                //InputValue = false; //Reset the input to false when exiting
            }
        }

        public virtual void OnModeStateMove(AnimatorStateInfo stateInfo, Animator anim, int Layer)
        {
            ActivatebyInput();

            if (modifier != null)
            {
                modifier.OnModeMove(this, stateInfo, anim, Layer);
            }
        }


        public void ActivatebyInput()
        {
            if (animal.LockInput) return;            //Do no Activate if is sleep or disable or lock input is Enable;

            if (InputValue)
            {
                TryActivate();
            }
        }


        /// <summary> Check for Exiting the Mode, If the animal changed to a new state and the Affect list has some State</summary>
        public virtual void StateChanged(StateID ID)
        {
            var affect = AbilityOverride ? ActiveAbility.OverrideProperties.affect : GlobalProperties.affect;
           // Debug.Log("StateChanged");
            if (!AffectStates_Empty)
            {

                if (affect == AffectStates.Exclude && ContainState(ID)      //If the new state is on the Exclude State
                || (affect == AffectStates.Inlcude && !ContainState(ID)))   //OR If the new state is not on the Include State
                    animal.Mode_Interrupt();
            }
        }


        /// <summary>Find if a State ID is on the Avoid From list</summary>
        protected bool ContainState(StateID ID)
        {
            return GlobalProperties.affectStates.Contains(ID);
        }

        protected bool AffectStates_Empty 
        {
            get { return (GlobalProperties.affectStates == null || GlobalProperties.affectStates.Count == 0); }
        }

        protected IEnumerator Ability_By_Time(float time)
        {
          if (animal.debugModes) Debug.Log("ActiveByTime: " + time);
            yield return new WaitForSeconds(time);
            animal.Mode_Interrupt();
        }

        public bool AbilityOverride
        { get { return (ActiveAbility != null && ActiveAbility.OverrideProp); } }

        /// <summary> Active Ability Index of the mode</summary>
        public int AbilityIndex
        {
            get { return abilityIndex; }
            set
            {
                abilityIndex.Value = value;
                OnAbilityIndex.Invoke(value);
            }
        }

        private void OnExitInvoke()
        {
            if (AbilityOverride) ActiveAbility.OverrideProperties.OnExit.Invoke();
            GlobalProperties.OnExit.Invoke();
        }

        private void OnEnterInvoke()
        {
            if (AbilityOverride) ActiveAbility.OverrideProperties.OnEnter.Invoke();

            GlobalProperties.OnEnter.Invoke();
        }


        private bool CheckStatus(AbilityStatus status)
        {
            if (AbilityOverride)
            {
                return ActiveAbility.OverrideProperties.Status == status;
            }
            return GlobalProperties.Status == status;
        }


        //private void ModifyOnExit(MAnimal animal)
        //{
        //    if (AbilityOverride)
        //    {
        //        ActiveAbility.OverrideProperties.ModifyOnExit.Modify(animal);
        //    }
        //    else
        //    {
        //        GlobalProperties.ModifyOnExit.Modify(animal);
        //    }
        //}
        //private void ModifyOnEnter(MAnimal animal)
        //{
        //    if (AbilityOverride)
        //    {
        //        ActiveAbility.OverrideProperties.ModifyOnEnter.Modify(animal);
        //    }
        //    else
        //    {
        //        GlobalProperties.ModifyOnEnter.Modify(animal);
        //    }
        //}

    }

    [System.Serializable]
    public class Ability
    {
        /// <summary>Name of the Ability (Visual Only)</summary>
        public string Name;
        /// <summary>index of the Ability </summary>
        public IntReference Index;
        /// <summary>if true Overrides the Global properties on the Mode </summary>
        public bool OverrideProp;
        /// <summary>Overrides Properties on the mode</summary>
        public ModeProperties OverrideProperties;
 
    }

    public enum AbilityStatus
    {  
        /// <summary> The Ability is Enabled One time and Exit when the Animation is finished </summary>
        PlayOneTime = 0,
        /// <summary> The Ability is On while the Input is pressed</summary>
        HoldByInput = 1,
        /// <summary> The Ability is On for an x ammount of time</summary>
        HoldByTime = 2,
        /// <summary> The Ability is ON and OFF everytime the Activate method is called</summary>
        Toggle = 3,
    }
    public enum AffectStates
    {
        Inlcude,
        Exclude
    }

    [System.Serializable]
    public class ModeProperties
    {
        /// <summary>The Ability can Stay Active until it finish the Animation, by Holding the Input Down, by x time </summary>
        [Tooltip("The Ability can Stay Active until it finish the Animation, by Holding the Input Down, by x time ")]
        public AbilityStatus Status = AbilityStatus.PlayOneTime;
        /// <summary>The Ability can Stay Active by x seconds </summary>
        public FloatReference HoldByTime;
        /// <summary>Modify common Properties on the Animal when entering the Mode</summary>
        //[Tooltip("Modify common Properties on the Animal when entering the Mode")]
        //public AnimalModifier ModifyOnEnter;
        ///// <summary>Modify common Properties on the Animal when Exiting the Mode</summary>
        //[Tooltip("Modify common Properties on the Animal when Exiting the Mode")]
        //public AnimalModifier ModifyOnExit;
        ///// <summary>If Exlude then the Mode will not be Enabled when is on a State on the List, If Include, then the mode will only be active when the Animal is on a state on the List </summary>
        [Tooltip("If Exlude then the Mode will not be Enabled when is on a State on the List, If Include, then the mode will only be active when the Animal is on a state on the List")]
        public AffectStates affect;
        /// <summary>Include/Exclude the  States on this list depending the Affect variable</summary>
        [Tooltip("Include/Exclude the  States on this list depending the Affect variable")]
        public List<StateID> affectStates;

        /// <summary> The Abilty can be interrupted by another Ability after x time... if InterruptTime = 0 then it cannot be interrupted</summary>
        [Tooltip(" The Abilty can be interrupted by another Ability after x time... if InterruptTime = 0 then it cannot be interrupted")]
        public float InterruptTime = 0f;
    

        [SerializeField]  private bool ShowEvents;
        public UnityEvent OnEnter;
        public UnityEvent OnExit;
    }
}