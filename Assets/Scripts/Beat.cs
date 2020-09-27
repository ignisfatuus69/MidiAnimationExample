using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnOkayState : UnityEvent { };
[System.Serializable]
public class OnMissedState : UnityEvent { };
[System.Serializable]
public class OnPerfectState : UnityEvent { };
[System.Serializable]
public class OnEndState : UnityEvent <Beat> { };
public enum BeatState { Missed, Okay, Perfect,End };
public class Beat : MonoBehaviour
{

    public Animator BeatAnimator;
    public OnMissedState EVT_OnMissedState;
    public OnOkayState EVT_OnOkayState;
    public OnPerfectState EVT_OnPerfectState;
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
        BeatAnimator.Play("BeatAnimation", -1,  0.0f);

    }

    private void ActivateMissedState()
    {
        EVT_OnMissedState.Invoke();
        Status = BeatState.Missed;
    }

    private void ActivateOkayState()
    {
        EVT_OnOkayState.Invoke();
        Status = BeatState.Okay;
    }

    private void ActivatePerfectState()
    {
        EVT_OnOkayState.Invoke();
        Status = BeatState.Perfect;
    }

    private void ActivateEndState()
    {
        EVT_OnEndState.Invoke(this);
        Status = BeatState.End;
    }

    
}
