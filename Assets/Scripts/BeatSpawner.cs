using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]

public class OnBeatPooled : UnityEvent<Beat> { };
public class BeatSpawner : ObjectPooler
{
    public float BeatAnimationSpeed;
    public BeatInteractor[] BeatInteractorObjs;
    public OnBeatPooled EVT_OnBeatPooled;
    public Vector3[] RandomBeatPositions;

    //  public BeatManager BeatManagerObj;

    private void Start()
    {
        for (int i = 0; i < BeatInteractorObjs.Length; i++)
        {
            BeatInteractorObjs[i].EVT_OnBeatEvaluated.AddListener(OnDeactivate);
            //BeatInteractorObjs[i].EVT_OnBeatEvaluating.AddListener
        }
    }
    protected override void InitializeSpawnObject(GameObject obj)
    {
        //add for automatic pooling

        Beat BeatObj = obj.GetComponent<Beat>();

        BeatObj.EVT_OnEndState.AddListener(OnDeactivate);

        BeatObj.BeatAnimator.speed = this.BeatAnimationSpeed;
     
        

    }

    private void OnDeactivate(Beat BeatToDespawn)
    {

        //  EVT_OnBeatPooled.Invoke(BeatToDespawn);
        // Remove the beats
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
        SpawnPosition = RandomBeatPositions[randomNumber];
    }


}
