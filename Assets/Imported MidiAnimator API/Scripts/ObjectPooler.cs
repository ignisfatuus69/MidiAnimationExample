using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPooler : MonoBehaviour
{
	public GameObject ObjectToSpawn;
	public float SpawnInterval = 1.0f;
	public int SpawnCount = 1;
	public float Radius = 5.0f;

	public List<GameObject> currentSpawn = new List<GameObject>();
	protected List<GameObject> pooledObjects = new List<GameObject>();



	public void SpawnObject()
	{
	
			for (int i = 0; i < SpawnCount; i++)
			{

				// Spawn the object. If we have an object in the pool, use that instead. Else, instantiate.
				GameObject obj;
				if (pooledObjects.Count >0)
				{
					obj = pooledObjects[0];
					pooledObjects.RemoveAt(0);
					obj.SetActive(true);
					currentSpawn.Add(obj);
				}
				else
				{
					obj = Instantiate(ObjectToSpawn);
					currentSpawn.Add(obj);
				}

				float X = Random.Range(-7, 7);
				float y = Random.Range(-3.5f, 3.5f);
				// Randomize position
				obj.transform.position = new Vector3(X, y, -1);

			
				RemoveSpawn(obj);
			

			}

		
	}

	protected abstract void RemoveSpawn(GameObject obj);

	
}
