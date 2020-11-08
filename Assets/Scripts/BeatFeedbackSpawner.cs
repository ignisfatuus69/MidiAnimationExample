using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class OnBeatFeedbackTextSpawn : UnityEvent<Beat> { };
public class BeatFeedbackSpawner : ObjectPooler
{
    private string TextString;
    public BeatSpawner BeatSpawnerobj;
    public Transform CanvasParent;
    public float TimeToDespawn;
    public GameObject[] FeedbackObject;
    

    private void Start()
    {
        BeatSpawnerobj.EVT_OnBeatPooled.AddListener(InitializeObjectBasedOnBeat);
        BeatSpawnerobj.EVT_OnObjectPooled.AddListener(SpawnObjects);
    }
    protected override void InitializeSpawnObject(GameObject obj)
    {
     
        StartCoroutine(DespawnInSeconds(obj));
    }

    private void OnDeactivate(GameObject obj)
    {
        obj.gameObject.SetActive(false);
        pooledObjects.Add(obj.gameObject);
        currentSpawnedObjects.Remove(obj.gameObject);
    }

    private void InitializeObjectBasedOnBeat(Beat BeatObjReference)
    {
        if (BeatObjReference.Status == BeatState.Early) ObjectToSpawn = FeedbackObject[0];
        if (BeatObjReference.Status == BeatState.Okay) ObjectToSpawn = FeedbackObject[0];
        if (BeatObjReference.Status == BeatState.Perfect) ObjectToSpawn = FeedbackObject[0];
        if (BeatObjReference.Status == BeatState.Late) ObjectToSpawn = FeedbackObject[0];
        SpawnPosition = BeatObjReference.transform.position;
    }

    IEnumerator DespawnInSeconds(GameObject objToDespawn)
    {
        yield return new WaitForSeconds(TimeToDespawn);
        OnDeactivate(objToDespawn);
    }
}
