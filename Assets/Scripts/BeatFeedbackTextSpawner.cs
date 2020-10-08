﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class OnBeatFeedbackTextSpawn : UnityEvent<Beat> { };
public class BeatFeedbackTextSpawner : ObjectPooler
{
    private string TextString;
    public BeatManager BeatManagerObj;
    public BeatSpawner BeatSpawnerobj;
    public Transform CanvasParent;


    private void Start()
    {
        BeatSpawnerobj.EVT_OnBeatPooled.AddListener(InitializeTextParameters);
        BeatManagerObj.EVT_OnBeatChecked.AddListener(InitializeTextParameters);

        BeatSpawnerobj.EVT_OnObjectPooled.AddListener(SpawnObjects);
    }
    protected override void InitializeSpawnObject(GameObject obj)
    {
        obj.transform.SetParent(CanvasParent);
        Text BeatFeedbackText = obj.GetComponent<Text>();
        BeatFeedbackText.text = TextString;


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

    private void InitializeTextParameters(Beat BeatObjReference)
    {
        TextString = BeatObjReference.Status.ToString();
        SpawnPosition = RectTransformUtility.WorldToScreenPoint(Camera.main,BeatObjReference.transform.position);
        
    }
}
