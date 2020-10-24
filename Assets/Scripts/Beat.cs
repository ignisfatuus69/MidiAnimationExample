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

    public Animator BeatAnimator;
    public Animation SpriteAnimation;
    public OnEarlyState EVT_OnEarlyState;
    public OnOkayState EVT_OnOkayState;
    public OnPerfectState EVT_OnPerfectState;
    public OnLateState EVT_OnLateState;
    public OnEndState EVT_OnEndState;
    public bool IsInteractable = false;
    public BeatState Status { get; private set; }
    public int ScoreValue { get; private set; }
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
        BeatAnimator.enabled = false;
        EVT_OnEndState.Invoke(this);
        Status = BeatState.End;
    }

    
}
