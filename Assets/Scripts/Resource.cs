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
    public bool HasCap = false;
    public float Value;
    public float MaxValue, MinValue;
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

        Value -= subtractionValue;
        CapValue();
        EVT_OnResourceSubtracted.Invoke();
    }

    public virtual void AddValue(float additive)
    {

        Value += additive;
        CapValue();
        EVT_OnResourceAdded.Invoke();
    }

    private void CapValue()
    {
        if (!HasCap) return;
        if (Value >= MaxValue) Value = MaxValue;
        if (Value <= MinValue) Value = MinValue;

    }

}
