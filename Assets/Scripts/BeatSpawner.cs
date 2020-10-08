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
    protected override void InitializeSpawnObject(GameObject obj)
    {
        //add for automatic pooling
        Beat BeatObj = obj.GetComponent<Beat>();
        BeatObj.EVT_OnEndState.AddListener(OnDeactivate);
        BeatObj.EVT_OnLateState.AddListener(BeatManagerObj.EVT_OnLateBeat.Invoke);

        //add for manual pooling
        BeatManagerObj.EVT_OnDeactivateBeat.AddListener(OnDeactivate);

        // make interactable here?
        if (currentSpawnedObjects.Count<=1)
        {
            BeatObj.IsInteractable = true;
        }
    }

    private void OnDeactivate(Beat BeatToDespawn)
    {
  
        // Remove the beats
        BeatToDespawn.gameObject.SetActive(false);
        BeatToDespawn.EVT_OnLateState.RemoveListener(BeatManagerObj.EVT_OnLateBeat.Invoke);

     
        BeatToDespawn.GetComponent<Animator>().enabled = false;
        BeatManagerObj.EVT_OnDeactivateBeat.RemoveListener(OnDeactivate);
        pooledObjects.Add(BeatToDespawn.gameObject);
        currentSpawnedObjects.Remove(BeatToDespawn.gameObject);
        BeatToDespawn.EVT_OnEndState.RemoveListener(OnDeactivate);
        BeatToDespawn.IsInteractable = false;

    }

    public void RandomizeBeatPosition()
    {
        int randomNumber = Random.Range(0, RandomBeatPositions.Length);
        SpawnPosition = RandomBeatPositions[randomNumber];
    }


}
