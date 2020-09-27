using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSpawner : ObjectPooler
{

    public BeatManager BeatManagerObj;
    void Start()
    {
        SpawnObject();
        SpawnObject();
        SpawnObject();
        SpawnObject();
        SpawnObject();


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
        BeatToDespawn.GetComponent<Animator>().enabled = false;
        BeatManagerObj.EVT_OnDeactivateBeat.RemoveListener(OnDeactivate);


        currentSpawn.Remove(BeatToDespawn.gameObject);

    }
}
