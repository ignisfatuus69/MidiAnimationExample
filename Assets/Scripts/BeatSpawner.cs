using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSpawner : ObjectPooler
{
    public Player PlayerObj;
    public BeatManager BeatManagerObj;
    void Start()
    {
        StartCoroutine(SpawnTask());
        PlayerObj.EVT_OnBeatClicked.AddListener(OnDeactivate);

    }
    protected override void RemoveSpawn(GameObject obj)
    {
        //add for automatic pooling
        Beat BeatObj = obj.GetComponent<Beat>();
        BeatObj.EVT_OnEndState.AddListener(OnDeactivate);

        //add for manual pooling
        BeatManagerObj.EVT_OnDeactivateBeat.AddListener(OnDeactivate);


    }

    void OnDeactivate(Beat BeatToDespawn)
    {
  
        // Remove the beats
        BeatToDespawn.gameObject.SetActive(false);
        pooledObjects.Add(BeatToDespawn.gameObject);
        BeatToDespawn.EVT_OnEndState.RemoveListener(OnDeactivate);

        BeatManagerObj.EVT_OnDeactivateBeat.RemoveListener(OnDeactivate);


        currentSpawn.Remove(BeatToDespawn.gameObject);

    }
}
