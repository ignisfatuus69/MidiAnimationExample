using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Events are based on managing the beats and checking results
[System.Serializable]
public class OnDeactivateBeat : UnityEvent <Beat> { };
[System.Serializable]
public class OnEarlyBeat : UnityEvent { };
[System.Serializable]
public class OnOkayBeat : UnityEvent { internal void AddListener(object v) { throw new NotImplementedException(); } };
[System.Serializable]
public class OnPerfectBeat : UnityEvent { };

[System.Serializable]
public class OnLateBeat : UnityEvent { };
public class BeatManager : MonoBehaviour
{
    public Tongatong[] TongatongObj;
    public OnOkayBeat EVT_OnOkayBeat;
    public OnPerfectBeat EVT_OnPerfectBeat;
    public OnEarlyBeat EVT_OnEarlyBeat;
    public OnDeactivateBeat EVT_OnDeactivateBeat;
    public OnLateBeat EVT_OnLateBeat;
    public BeatClicker PlayerObj;
    public int OkayScore;
    public int PerfectScore;

    // Start is called before the first frame update
    void Start()
    {
        PlayerObj.EVT_OnBeatClicked.AddListener(CheckForState);
        for (int i = 0; i < 4; i++)
        {
            TongatongObj[i].EVT_OnTongatongMove.AddListener(CheckForState);
        }
    }


    //Evaluates based on clicking
    public void CheckForState(Beat BeatObj)
    {
        if (BeatObj.Status == BeatState.Early)
        {
            // is having two of them fine?
            BeatObj.EVT_OnEarlyState.Invoke();
            EVT_OnEarlyBeat.Invoke();
            return;
        }

        else if (BeatObj.Status == BeatState.Okay)
        {
            BeatObj.EVT_OnOkayState.Invoke();
            EVT_OnOkayBeat.Invoke();
            EVT_OnDeactivateBeat.Invoke(BeatObj);

        }
        else if (BeatObj.Status == BeatState.Perfect)
        {
            BeatObj.EVT_OnPerfectState.Invoke();
            EVT_OnPerfectBeat.Invoke();
            EVT_OnDeactivateBeat.Invoke(BeatObj);
        }
    }


}
