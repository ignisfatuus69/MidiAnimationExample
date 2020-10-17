using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnTongatongHit : UnityEvent<Beat> { };
public class Tongatong : MonoBehaviour
{
    public KeyCode InputCode;
    public OnTongatongHit EVT_OnTongatongHit;
    public float speed = 5;
    public float displacement = 20;
    private Vector2 InitialPosition;

    // Start is called before the first frame update
    void Start()
    {
        speed *= Time.fixedDeltaTime;
        displacement *= Time.fixedDeltaTime;
        InitialPosition = transform.position;
    }

    public void MoveDown()
    {
        StopAllCoroutines();
        StartCoroutine(MoveToDisplacement(displacement));
    }

    IEnumerator MoveToDisplacement(float displacementOverTime)
    {
        Vector2 targetPosition = new Vector2(transform.position.x, InitialPosition.y - displacementOverTime);

        float distance = Vector2.Distance(transform.position, targetPosition);

        while (Vector2.Distance(transform.position, targetPosition) > 0.1)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;

        //object snaps back up
        if (displacementOverTime > 0)
        {
            StartCoroutine(MoveToDisplacement(0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(InputCode))
        {
            MoveDown();
     
    
            Ray ray = new Ray(this.transform.position, Vector3.down);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.green);
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 25))
            {

                Beat BeatHit = hit.transform.gameObject.GetComponent<Beat>();
                CheckForState(BeatHit);

            }

        }
    }

    void CheckForState(Beat BeatHit)
    {
        EVT_OnTongatongHit.Invoke(BeatHit);
        //if (BeatHit.Status == BeatState.Early)
        //{
        //    EVT_OnTongatongHit.Invoke(BeatHit);
        //    Debug.Log("Early");
        //}

        //else if (BeatHit.Status == BeatState.Okay)
        //{
        //    EVT_OnTongatongHit.Invoke(BeatHit);
        //    Debug.Log("Okay");

        //}
        //else if (BeatHit.Status == BeatState.Perfect)
        //{
        //    EVT_OnTongatongHit.Invoke(BeatHit);
        //    Debug.Log("Perfect");
        //}


    }

}
