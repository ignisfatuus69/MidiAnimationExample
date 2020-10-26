using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnBeatClicked : UnityEvent<Beat> { };

public class BeatClicker : BeatInteractor
{
    public OnBeatClicked EVT_OnBeatClicked;
    // Update is called once per frame
    void Update()
    {
        ClickBeat();
    }

 
    void ClickBeat()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var v3 = Input.mousePosition;
            v3.z = -10;
            Ray ray = Camera.main.ScreenPointToRay(v3);
 
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.green);
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 50))
            {
                
                BeatNode Node = hit.transform.gameObject.GetComponent<BeatNode>();
                EvaluateBeatNode(Node);
            }
        }
    }

  
}
