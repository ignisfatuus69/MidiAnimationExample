using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnDeactivateBeat : UnityEvent <Beat> { };
public class BeatManager : MonoBehaviour
{
    public OnDeactivateBeat EVT_OnDeactivateBeat;
    public Player PlayerObj;
    public int ScoreValue { get; private set; }
    public int OkayScore;
    public int PerfectScore;

    // Start is called before the first frame update
    void Start()
    {
        PlayerObj.EVT_OnBeatClicked.AddListener(CheckForState);
    }


    //Only for Clicking
    public void CheckForState(Beat BeatObj)
    {
        if (BeatObj.Status == BeatState.Missed)
        {
            //Switch animations adn shit
            //automatically gets pooled afterwards so no need to pool
            BeatObj.EVT_OnMissedState.Invoke();
            return;
        }


        if (BeatObj.Status == BeatState.Okay)
        {
            BeatObj.EVT_OnOkayState.Invoke();
            PlayerObj.Score += OkayScore;
  
            EVT_OnDeactivateBeat.Invoke(BeatObj);

        }
        if (BeatObj.Status == BeatState.Perfect)
        {
            BeatObj.EVT_OnPerfectState.Invoke();
            PlayerObj.Score += PerfectScore;

              EVT_OnDeactivateBeat.Invoke(BeatObj);
        }
    }








    }
