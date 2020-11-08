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
    public Sprite[] FeedbackSprites;
    private BeatState CurrentBeatStatus;

    private void Start()
    {
        BeatSpawnerobj.EVT_OnBeatPooled.AddListener(InitializeObjectBasedOnBeat);
        BeatSpawnerobj.EVT_OnObjectPooled.AddListener(SpawnObjects);
    }
    protected override void InitializeSpawnObject(GameObject obj)
    {

        Debug.Log(CurrentBeatStatus);
  
        if (CurrentBeatStatus == BeatState.Early)
        {
            obj.GetComponent<SpriteSetter>().SetSprites(FeedbackSprites[0],null);
        }
        if (CurrentBeatStatus == BeatState.Okay)
        {
            obj.GetComponent<SpriteSetter>().SetSprites(FeedbackSprites[1], FeedbackSprites[2]);
        }
        if (CurrentBeatStatus == BeatState.Perfect)
        {
            obj.GetComponent<SpriteSetter>().SetSprites(FeedbackSprites[3], FeedbackSprites[4]);
        }
        if (CurrentBeatStatus == BeatState.Late)
        {
            obj.GetComponent<SpriteSetter>().SetSprites(FeedbackSprites[0], null);
        }

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
        SpawnPosition = BeatObjReference.transform.position;
        CurrentBeatStatus = BeatObjReference.Status;
    }



    IEnumerator DespawnInSeconds(GameObject objToDespawn)
    {
        yield return new WaitForSeconds(TimeToDespawn);
        OnDeactivate(objToDespawn);
    }
}
