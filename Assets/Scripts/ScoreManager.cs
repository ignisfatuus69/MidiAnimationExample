using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ScoreManager : MonoBehaviour
{
    public Resource ScoreResource;
    public BeatManager BeatManagerObj;
    public BeatSpawner BeatSpawnerObj;
    public float OkayScore;
    public float PerfectScore;

    // Start is called before the first frame update
    void Start()
    {

        BeatSpawnerObj.EVT_OnBeatPooled.AddListener(AddScore);


    }

    private void AddScore(Beat BeatObj)
    {
        ScoreResource.AddValue(BeatObj.ScoreValue);
    }

 

    //private void AddScoreToBeat(Beat beatobj)
    //{
    //    ScoreResource.Value += OkayScore;
    //}
   
}
