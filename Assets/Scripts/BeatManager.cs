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
     //   if (BeatObj.Status == BeatState.Missed) 

     //   EVT_OnDeactivateBeat.Invoke(BeatObj);
        if (BeatObj.Status == BeatState.Okay)
        {
            PlayerObj.Score += OkayScore;

        }
        if (BeatObj.Status == BeatState.Perfect)
        {

            PlayerObj.Score += PerfectScore;
        }
    }








    }
