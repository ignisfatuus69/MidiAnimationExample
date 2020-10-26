using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]

public class OnBeatPooled : UnityEvent<Beat> { };
public class BeatSpawner : ObjectPooler
{
    public bool IsSpawningOnRandomPosition;
    public float BeatAnimationSpeed;

    public BeatInteractor[] BeatInteractorObjs;
    public OnBeatPooled EVT_OnBeatPooled;
    public BeatNode[] BeatContainers;
    public Vector3[] RandomBeatPositions;
    public int[] PositionCounters;
    private List<int> lastSpawnIndexes = new List<int>();
    //  public BeatManager BeatManagerObj;

    private void Start()
    {
        for (int i = 0; i < BeatInteractorObjs.Length; i++)
        {
            BeatInteractorObjs[i].EVT_OnBeatEvaluated.AddListener(OnDeactivate);
            //BeatInteractorObjs[i].EVT_OnBeatEvaluating.AddListener
        }

        for (int i = 0; i < PositionCounters.Length; i++)
        {
            PositionCounters[i] = 0;
        }
    }
    protected override void InitializeSpawnObject(GameObject obj)
    {
        //add for automatic pooling
        Beat BeatObj = obj.GetComponent<Beat>();
        if (IsSpawningOnRandomPosition)
        {
            int randomNumber = Random.Range(0, RandomBeatPositions.Length);
            SpawnPosition = BeatContainers[randomNumber].Position;


            PositionCounters[randomNumber] += 1;
            BeatContainers[randomNumber].AddBeat(BeatObj);
            lastSpawnIndexes.Add(randomNumber);
        }
       

        //call it here get the value across somehow
        BeatObj.EVT_OnEndState.AddListener(OnDeactivate);

        BeatObj.BeatAnimator.speed = this.BeatAnimationSpeed;



    }
  
    private void OnDeactivate(Beat BeatToDespawn)
    {

        //  EVT_OnBeatPooled.Invoke(BeatToDespawn);
        // Remove the beats
        if (IsSpawningOnRandomPosition)
        { 
        PositionCounters[lastSpawnIndexes[0]] -= 1;
        BeatContainers[lastSpawnIndexes[0]].RemoveBeat(BeatToDespawn);
        lastSpawnIndexes.RemoveAt(0);
        }


        EVT_OnBeatPooled.Invoke(BeatToDespawn);
        EVT_OnObjectPooled.Invoke();
        BeatToDespawn.BeatAnimator.speed = 1;
        BeatToDespawn.gameObject.SetActive(false);

        BeatToDespawn.EVT_OnEndState.RemoveListener(OnDeactivate);
        pooledObjects.Add(BeatToDespawn.gameObject);
        currentSpawnedObjects.Remove(BeatToDespawn.gameObject);
            
    }

    public void RandomizeBeatPosition()
    {
        int randomNumber = Random.Range(0, RandomBeatPositions.Length);
        SpawnPosition = BeatContainers[randomNumber].Position;


        PositionCounters[randomNumber] += 1;

        lastSpawnIndexes.Add(randomNumber);
    }


}
