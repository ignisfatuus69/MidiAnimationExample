using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testlang : MonoBehaviour
{
    public BeatSpawner BeatSpawnerobj;
    public GameObject BeatToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PangSpawnLnag());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Printpare()
    {

    }

    IEnumerator PangSpawnLnag()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            BeatSpawnerobj.RandomizeBeatPosition();
            BeatSpawnerobj.SpawnObjects();
        }
    }
}
