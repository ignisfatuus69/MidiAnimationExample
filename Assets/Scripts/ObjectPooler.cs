using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnObjectPooled : UnityEvent { };

[System.Serializable]
public class OnObjectSpawned : UnityEvent { };
public abstract class ObjectPooler : MonoBehaviour
{
	public OnObjectSpawned EVT_OnObjectSpawned;
	public OnObjectPooled EVT_OnObjectPooled;
	public GameObject ObjectToSpawn;
	public float SpawnInterval = 1.0f;
	public int SpawnCount = 1;
	public float Radius = 5.0f;

	protected List<GameObject> currentSpawnedObjects = new List<GameObject>();

	protected List<GameObject> pooledObjects = new List<GameObject>();
	public Vector3 SpawnPosition;


	public void SpawnObjects()
	{
			for (int i = 0; i < SpawnCount; i++)
			{

				// Spawn the object. If we have an object in the pool, use that instead. Else, instantiate.
				GameObject obj;
				if (pooledObjects.Count >0)
				{
				// get the last pooled object
					obj = pooledObjects[pooledObjects.Count - 1];
					pooledObjects.RemoveAt(pooledObjects.Count-1);
					obj.SetActive(true);
					currentSpawnedObjects.Add(obj);
					EVT_OnObjectSpawned.Invoke();
				}
				else
				{
					obj = Instantiate(ObjectToSpawn);
					currentSpawnedObjects.Add(obj);
					EVT_OnObjectSpawned.Invoke();
				}

			// Randomize position
			obj.transform.position = SpawnPosition;

				InitializeSpawnObject(obj);
			}
	}

	protected abstract void InitializeSpawnObject(GameObject obj);

	
}
