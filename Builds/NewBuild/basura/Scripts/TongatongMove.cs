using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongatongMove : MonoBehaviour
{
    public float speed = 5;
    public float displacement = 20;
    private Vector2 InitialPosition;
    // Start is called before the first frame update
    void Start()
    {
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

    }
}
