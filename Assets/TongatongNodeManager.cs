using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongatongNodeManager : MonoBehaviour
{
    [SerializeField]
    public int[] SpawnCounts;
    private Vector3[] SpawnPoints;
    public BeatSpawner BeatSpawnerObj;
    public Beat BeatReference;
    private void Start()
    {
        for (int i = 0; i < SpawnCounts.Length; i++)
        {
            SpawnCounts[i] = 0;
        }
        SpawnPoints = BeatSpawnerObj.RandomBeatPositions;
        BeatSpawnerObj.EVT_OnObjectSpawned.AddListener(CheckNodePosition);
    }

 
    void CheckNodePosition()
    {
        Debug.Log("wtf");
        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            CheckPositions(SpawnPoints[i], Vector3.forward,SpawnCounts[i]);
        }
    
    }

    private void CheckPositions(Vector3 Position, Vector3 Direction,int SpawnCount)
    {
        Ray ray = new Ray(Position, Direction);
        ray.origin = new Vector3(ray.origin.x, ray.origin.y, -12);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 25, Color.green);
        if (Physics.Raycast(ray.origin, ray.direction, out hit, 25))
        {
            Beat BeatHit = hit.transform.gameObject.GetComponent<Beat>();
            BeatReference = BeatHit;
            Debug.Log("Hit");
            SpawnCount += 1;
        }
    }
}
