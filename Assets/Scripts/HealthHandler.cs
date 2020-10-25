using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnHealthUpdated : UnityEvent { };

public class HealthHandler : MonoBehaviour
{

    public BeatSpawner BeatSpawnerObj;
    public Resource PlayerHealth;
    public float MissedValue;
    public float OkayValue;
    public float PerfectValue;
    private void Start()
    {
        BeatSpawnerObj.EVT_OnBeatPooled.AddListener(ModifyHealthOnBeatInteraction);
    }

    private void ModifyHealthOnBeatInteraction(Beat BeatInteracted)
    {
        if (BeatInteracted.Status == BeatState.Early)
        {
            PlayerHealth.ReduceValue(MissedValue);
        }
        if (BeatInteracted.Status == BeatState.Okay)
        {
            PlayerHealth.AddValue(OkayValue);
        }
        if (BeatInteracted.Status == BeatState.Perfect)
        {
            PlayerHealth.AddValue(PerfectValue);
        }
        if (BeatInteracted.Status == BeatState.Late)
        {
            PlayerHealth.ReduceValue(MissedValue);
        }
        if (BeatInteracted.Status== BeatState.End)
        {
            PlayerHealth.ReduceValue(MissedValue);
        }

    }







}
