using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ScoreManager : MonoBehaviour
{
    public Resource ScoreResource;
    public BeatManager BeatManagerObj;
    public float OkayScore;
    public float PerfectScore;

    // Start is called before the first frame update
    void Start()
    {
        BeatManagerObj.EVT_OnOkayBeat.AddListener(AddOkayScore);
        BeatManagerObj.EVT_OnPerfectBeat.AddListener(AddPerfectScore);
     //   BeatManagerObj.EVT_OnBeatChecked.AddListener(AddScoreToBeat);

    }

    private void AddOkayScore()
    {
        ScoreResource.AddValue(OkayScore);
    }

    private void AddPerfectScore()
    {
        ScoreResource.AddValue(PerfectScore);
    }

    //private void AddScoreToBeat(Beat beatobj)
    //{
    //    ScoreResource.Value += OkayScore;
    //}
   
}
