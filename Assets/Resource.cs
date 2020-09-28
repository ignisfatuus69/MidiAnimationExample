using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnResourceAdded : UnityEvent { };

[System.Serializable]
public class OnResourceSubtracted : UnityEvent { };

[System.Serializable]
public class OnResourceInitialized : UnityEvent { };

public class Resource : MonoBehaviour
{

    public float Value;
    public OnResourceAdded EVT_OnResourceAdded;
    public OnResourceSubtracted EVT_OnResourceSubtracted;
    public OnResourceInitialized EVT_OnResourceInitialized;


    // Start is called before the first frame update
    public virtual void Start()
    {
        EVT_OnResourceInitialized.Invoke();
    }

    public virtual void ReduceValue(float subtractionValue)
    {
        EVT_OnResourceSubtracted.Invoke();
        Value -= subtractionValue;
    }

    public virtual void AddValue(float additive)
    {
        EVT_OnResourceAdded.Invoke();
        Value += additive;
    }

}
