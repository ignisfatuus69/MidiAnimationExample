using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnBeatClicked : UnityEvent<Beat> { };

public class Player : MonoBehaviour
{
    public OnBeatClicked EVT_OnBeatClicked;
    public Beat BeatGameObj;
    public int Score;
    public ObjectPooler BeatSpawner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DeleteBeat();
    }

 
    void DeleteBeat()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var v3 = Input.mousePosition;
            v3.z = -10;
            Ray ray = Camera.main.ScreenPointToRay(v3);


 
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.green);
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 25))
            {
                
                Beat BeatHit = hit.transform.gameObject.GetComponent<Beat>();
                BeatGameObj = BeatHit;
                BeatGameObj.gameObject.SetActive(false);
                EVT_OnBeatClicked.Invoke(BeatHit);
                Debug.Log("clicked once");
            }


        }
    }

  
}
