using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSpawner : ObjectPooler
{
    public Vector3[] RandomBeatPositions;
    public BeatManager BeatManagerObj;
    void Start()
    {
       // Spawn Objects before hand as extra objects
        //for (int i = 0; i < 5; i++)
        //{
        //    SpawnObjects();
        //}


    }
    protected override void InitializedRemovingSpawn(GameObject obj)
    {
        //add for automatic pooling
        Beat BeatObj = obj.GetComponent<Beat>();
        BeatObj.EVT_OnEndState.AddListener(OnDeactivate);
        //not sure about this pare
        BeatObj.EVT_OnLateState.AddListener(BeatManagerObj.EVT_OnLateBeat.Invoke);
        //add for manual pooling
        BeatManagerObj.EVT_OnDeactivateBeat.AddListener(OnDeactivate);
    }

    private void OnDeactivate(Beat BeatToDespawn)
    {
  
        // Remove the beats
        BeatToDespawn.gameObject.SetActive(false);
        BeatToDespawn.EVT_OnLateState.RemoveListener(BeatManagerObj.EVT_OnLateBeat.Invoke);
     
        BeatToDespawn.GetComponent<Animator>().enabled = false;
        BeatManagerObj.EVT_OnDeactivateBeat.RemoveListener(OnDeactivate);
        pooledObjects.Add(BeatToDespawn.gameObject);
        currentSpawn.Remove(BeatToDespawn.gameObject);
        BeatToDespawn.EVT_OnEndState.RemoveListener(OnDeactivate);
    }

    public void RandomizeBeatPosition()
    {
        int randomNumber = Random.Range(0, RandomBeatPositions.Length);
        SpawnPosition = RandomBeatPositions[randomNumber];
    }
}
