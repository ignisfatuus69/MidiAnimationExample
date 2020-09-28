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
    public OnEarlyState EVT_OnEarlyState;
    public OnOkayState EVT_OnOkayState;
    public OnPerfectState EVT_OnPerfectState;
    public OnLateState EVT_OnLateState;
    public OnEndState EVT_OnEndState;
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

    }

    private void ActivateEarlyState()
    {
        Status = BeatState.Early;
    }

    private void ActivateOkayState()
    {
 
        Status = BeatState.Okay;
    }

    private void ActivatePerfectState()
    {

        Status = BeatState.Perfect;
    }

    private void ActivateLateState()
    {
        EVT_OnLateState.Invoke();
        Status = BeatState.Late;
    }

    private void ActivateEndState()
    {
        EVT_OnEndState.Invoke(this);
        Status = BeatState.End;
    }

    
}
