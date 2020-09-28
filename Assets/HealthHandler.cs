using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class HealthHandler : MonoBehaviour
{
    public BeatManager BeatManagerObj;
    public Resource PlayerHealth;
    public float MissedValue;
    public float OkayValue;
    public float PerfectValue;
    private void Start()
    {
        //Reduces health both from clicking too early and not being able to click on time
        BeatManagerObj.EVT_OnEarlyBeat.AddListener(ReduceHealthValueOnMiss);
        BeatManagerObj.EVT_OnLateBeat.AddListener(ReduceHealthValueOnMiss);
        BeatManagerObj.EVT_OnOkayBeat.AddListener(AddHealthValueOnOkay);
        BeatManagerObj.EVT_OnPerfectBeat.AddListener(AddHealthValueOnPerfect);
    }

    private void ReduceHealthValueOnMiss()
    {
        Debug.Log("Health Reduced");
        PlayerHealth.ReduceValue(this.MissedValue);
    }

    private void AddHealthValueOnOkay()
    {
        PlayerHealth.AddValue(this.OkayValue);
    }

    private void AddHealthValueOnPerfect()
    {
        PlayerHealth.AddValue(this.PerfectValue);
    }





}
