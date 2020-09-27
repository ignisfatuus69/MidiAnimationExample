using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testlang : MonoBehaviour
{
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
        float x = Random.Range(-9, 9);
        float y = Random.Range(-5, 5);
        Instantiate(BeatToSpawn, new Vector3(x,y,1),Quaternion.identity);
    }

    IEnumerator PangSpawnLnag()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Printpare();
        }
    }
}
