using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Events are based on the state
[System.Serializable]
public class OnOkayState : UnityEvent { };
[System.Serializable]
public class OnEarlyState : UnityEvent { };
[System.Serializable]
public class OnPerfectState : UnityEvent { };
[System.Serializable]
public class OnLateState : UnityEvent { };
[System.Serializable]
public class OnEndState : UnityEvent <Beat> { };

public enum BeatState { Early, Okay, Perfect,Late,End };
public class Beat : MonoBehaviour
{
 
    public Sequencer SequencerRef;
    public Animator BeatAnimator;
    public Animation SpriteAnimation;
    public OnEarlyState EVT_OnEarlyState;
    public OnOkayState EVT_OnOkayState;
    public OnPerfectState EVT_OnPerfectState;
    public OnLateState EVT_OnLateState;
    public OnEndState EVT_OnEndState;
    public SphereCollider BeatCollider;
    public bool IsInteractable = false;
    public BeatState Status { get;  set; }
    public int ScoreValue { get; private set; }
    public double CurrentTimeStamp;
    public int Index = 0;
    // Start is called before the first frame update
    void Start()
    {
        ScoreValue = 0;
    }

    private void OnEnable()
    {

        ScoreValue = 0;
        BeatAnimator.enabled = true;
        BeatAnimator.Play(0, -1, 0);
    }

    private void OnDisable()
    {
        BeatAnimator.playbackTime = 0;
        IsInteractable = false;
        this.BeatCollider.enabled = false;
    }

    private void ActivateEarlyState()
    {
        ScoreValue = 0;
      //  EVT_OnEarlyState.Invoke();
        Status = BeatState.Early;
    }

    private void ActivateOkayState()
    {
        ScoreValue = 100;
      //  EVT_OnOkayState.Invoke();
        Status = BeatState.Okay;
    }

    private void ActivatePerfectState()
    {
        ScoreValue = 300;
    //    EVT_OnPerfectState.Invoke();
        
        Status = BeatState.Perfect;
    }

    private void ActivateLateState()
    {
        ScoreValue = 0;
     //   EVT_OnLateState.Invoke();
        Status = BeatState.Late;
    }

    private void ActivateEndState()
    {
        ScoreValue = 0;
        BeatAnimator.enabled = false;
        Status = BeatState.Late;
        EVT_OnEndState.Invoke(this);

    }

    private void ActivateStates()
    {
        // Early State
        if (SequencerRef.PlayableDirectorObj.time > (this.CurrentTimeStamp + (SequencerRef.OffSetBeatTime / 3.25f))
            && SequencerRef.PlayableDirectorObj.time < (this.CurrentTimeStamp + (SequencerRef.OffSetBeatTime / 2.5f)))
        {

            ActivateEarlyState();
        }
        // Okay State
        if (SequencerRef.PlayableDirectorObj.time > (this.CurrentTimeStamp + (SequencerRef.OffSetBeatTime / 2.5f))
            && SequencerRef.PlayableDirectorObj.time < (this.CurrentTimeStamp + (SequencerRef.OffSetBeatTime / 1.5f)))
        {
            ActivateOkayState();
        }
        // Perfect State
        if (SequencerRef.PlayableDirectorObj.time > (this.CurrentTimeStamp + (SequencerRef.OffSetBeatTime / 1.5f))
            && SequencerRef.PlayableDirectorObj.time < this.CurrentTimeStamp + SequencerRef.OffSetBeatTime)
        {

            ActivatePerfectState();
        }
        // Late/End State
        if (SequencerRef.PlayableDirectorObj.time > this.CurrentTimeStamp + SequencerRef.OffSetBeatTime)
        {
            ActivateEndState();
        }

    }

    private void EnableBasedOnTimestamp()
    {
       

        if (this.Index == 0)
        {
            Debug.Log("true");
            this.IsInteractable = true;
        }
            // second beat until the last beat interactable is being managed

        if (this.Index > 0 && this.Index
           <= (SequencerRef.loadedTimeStamp.TimeStampsNumbers.Count-1))
        {
            // if the current time is greater than the last beat has finished, make the current beat interactable
            if (SequencerRef.PlayableDirectorObj.time
                >= (SequencerRef.loadedTimeStamp.TimeStampsNumbers[this.Index - 1] + SequencerRef.OffSetBeatTime))
            {
                this.IsInteractable = true;
                this.BeatCollider.center = new Vector3(0, 0, 1);
                return;
            }
            else
            {
                this.BeatCollider.center = new Vector3(0, 0, 0);
                this.IsInteractable = false;
               
            }
        }

        //{
        //    this.IsInteractable = true;
        //    // if the current time is greater than the last beat has finished, make the current beat interactable
        //    //THIS LINE GETS THE EXACT TIME WHEN IT SHOULD BE ENABLED  //Debug.Log(SequencerRef.loadedTimeStamp.TimeStampsNumbers[SequencerRef.index - 2] + SequencerRef.OffSetBeatTime);
        //    if (SequencerRef.PlayableDirectorObj.time
        //       >= (SequencerRef.loadedTimeStamp.TimeStampsNumbers[SequencerRef.index - 2] + SequencerRef.OffSetBeatTime))
        //    {
        //        Debug.Log(SequencerRef.loadedTimeStamp.TimeStampsNumbers[SequencerRef.index - 2] + SequencerRef.OffSetBeatTime);
        //        this.IsInteractable = true;
        //        return;

            //    }
            //    else
            //    {
            //        this.IsInteractable = false;
            //    }
            //}
            //   this.IsInteractable = true;
            //if (SequencerRef.index == 0)    
            //{
            //    this.IsInteractable = true;
            //    return;
            //}
            //else
            //{
            //    if (SequencerRef.PlayableDirectorObj.time
            //       >= (SequencerRef.loadedTimeStamp.TimeStampsNumbers[SequencerRef.index - 1] + SequencerRef.OffSetBeatTime))
            //    {
            //        this.IsInteractable = true;
            //        return;
            //    }
            //    else
            //    {
            //        this.IsInteractable = false;
            //    }
            //}
    }

    private void Update()
    {
        EnableBasedOnTimestamp();
        ActivateStates();

    }




}
