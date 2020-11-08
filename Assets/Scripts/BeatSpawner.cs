using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]

public class OnBeatPooled : UnityEvent<Beat> { };

[System.Serializable]
public class OnBeatSpawned : UnityEvent<Beat> { };
public class BeatSpawner : ObjectPooler
{
    public bool IsSpawningOnRandomPosition;
    public float BeatAnimationSpeed;

    public Sequencer SequencerObj;
    public BeatInteractor[] BeatInteractorObjs;
    public OnBeatPooled EVT_OnBeatPooled;
    public OnBeatSpawned EVT_OnBeatSpawned;

    public Vector3[] BeatSpawnPoints;
    private int[] SpawnIndexes;
    private int BeatsSpawned = 0;
    //  public BeatManager BeatManagerObj;

    private void Start()
    {
        for (int i = 0; i < BeatInteractorObjs.Length; i++)
        {
            BeatInteractorObjs[i].EVT_OnBeatEvaluated.AddListener(OnDeactivate);
        }
        SequencerObj.EVT_OnBeatTimedUp.AddListener(OnDeactivate);
        SpawnIndexes = SequencerObj.BeatSequencerInfo.SpawnPointIndex.ToArray();
    }
    protected override void InitializeSpawnObject(GameObject obj)
    {
        //add for automatic pooling

        Beat BeatObj = obj.GetComponent<Beat>();

        SetBeatPosition();
        BeatObj.SequencerRef = this.SequencerObj;

        BeatObj.EVT_OnEndState.AddListener(OnDeactivate);

        BeatObj.BeatAnimator.speed = this.BeatAnimationSpeed;

        EVT_OnBeatSpawned.Invoke(BeatObj);
    }
  
    private void OnDeactivate(Beat BeatToDespawn)
    {

        EVT_OnBeatPooled.Invoke(BeatToDespawn);
        EVT_OnObjectPooled.Invoke();
        BeatToDespawn.BeatAnimator.speed = 1;
        BeatToDespawn.gameObject.SetActive(false);

        BeatToDespawn.EVT_OnEndState.RemoveListener(OnDeactivate);
        pooledObjects.Add(BeatToDespawn.gameObject);
        currentSpawnedObjects.Remove(BeatToDespawn.gameObject);
            
    }

    public void SetBeatPosition()
    {
        SpawnPosition = BeatSpawnPoints[SpawnIndexes[BeatsSpawned]];
        BeatsSpawned += 1;


    }


}
