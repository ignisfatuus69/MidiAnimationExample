using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ParticleSpawner : ObjectPooler
{
    public float ParticleDuration;
    public BeatSpawner BeatSpawnerObj;
    public BeatState BeatStatusCondition;

    private void Start()
    {
        BeatSpawnerObj.EVT_OnBeatPooled.AddListener(SpawnObjectBasedOnBeat);
    }
    protected override void InitializeSpawnObject(GameObject obj)
    {
        
        StartCoroutine(DespawnInDuration(obj));
    }

    private void SpawnObjectBasedOnBeat(Beat BeatObjReference)
    {
        SpawnPosition = BeatObjReference.transform.position;
        if (BeatObjReference.Status == this.BeatStatusCondition)
        {
            SpawnObjects();
        }
    }
    private void OnDeactivate(GameObject obj)
    {
        obj.gameObject.SetActive(false);
        pooledObjects.Add(obj.gameObject);
        currentSpawnedObjects.Remove(obj.gameObject);
    }
    IEnumerator DespawnInDuration(GameObject objToDespawn)
    {
        yield return new WaitForSeconds(ParticleDuration);
        OnDeactivate(objToDespawn);
    }

}
