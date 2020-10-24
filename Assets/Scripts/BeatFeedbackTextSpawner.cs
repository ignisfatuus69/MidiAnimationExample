using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class OnBeatFeedbackTextSpawn : UnityEvent<Beat> { };
public class BeatFeedbackTextSpawner : ObjectPooler
{
    private string TextString;
    public BeatInteractor[] BeatInteractorObjs;
    public BeatSpawner BeatSpawnerobj;
    public Transform CanvasParent;


    private void Start()
    {
     

        BeatSpawnerobj.EVT_OnBeatPooled.AddListener(InitializeTextParameters);
        BeatSpawnerobj.EVT_OnObjectPooled.AddListener(SpawnObjects);
    }
    protected override void InitializeSpawnObject(GameObject obj)
    {
        obj.transform.SetParent(CanvasParent);
        Text BeatFeedbackText = obj.GetComponent<Text>();
        BeatFeedbackText.text = TextString;
        StartCoroutine(DespawnInSeconds(obj));

    }

    private void OnDeactivate(GameObject obj)
    {


        obj.gameObject.SetActive(false);



 
        pooledObjects.Add(obj.gameObject);
        currentSpawnedObjects.Remove(obj.gameObject);



    }

    private void InitializeTextParameters(Beat BeatObjReference)
    {
        TextString = BeatObjReference.Status.ToString() + "  " + BeatObjReference.ScoreValue;
        SpawnPosition = RectTransformUtility.WorldToScreenPoint(Camera.main,BeatObjReference.transform.position);
        
    }

    IEnumerator DespawnInSeconds(GameObject objToDespawn)
    {
        yield return new WaitForSeconds(2);
        OnDeactivate(objToDespawn);
    }
}
