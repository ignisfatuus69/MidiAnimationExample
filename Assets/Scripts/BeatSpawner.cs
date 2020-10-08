using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]

public class OnBeatPooled : UnityEvent<Beat> { };
public class BeatSpawner : ObjectPooler
{
    public OnBeatPooled EVT_OnBeatPooled;
    public Vector3[] RandomBeatPositions;
    public BeatManager BeatManagerObj;
 
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

        EVT_OnBeatPooled.Invoke(BeatToDespawn);
        // Remove the beats
        BeatToDespawn.gameObject.SetActive(false);
        BeatToDespawn.EVT_OnLateState.RemoveListener(BeatManagerObj.EVT_OnLateBeat.Invoke);

     
        BeatToDespawn.GetComponent<Animator>().enabled = false;
        BeatManagerObj.EVT_OnDeactivateBeat.RemoveListener(OnDeactivate);
        pooledObjects.Add(BeatToDespawn.gameObject);
        currentSpawnedObjects.Remove(BeatToDespawn.gameObject);
        BeatToDespawn.EVT_OnEndState.RemoveListener(OnDeactivate);
        BeatToDespawn.IsInteractable = false;

        EVT_OnObjectPooled.Invoke();
    }

    public void RandomizeBeatPosition()
    {
        int randomNumber = Random.Range(0, RandomBeatPositions.Length);
        SpawnPosition = RandomBeatPositions[randomNumber];
    }


}
