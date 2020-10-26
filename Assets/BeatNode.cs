using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatNode :MonoBehaviour
{
    public Vector3 Position;
    public List<Beat> BeatsContained = new List<Beat>();
    private int Count = 0;
    public void AddBeat(Beat BeatObj)
    {
        BeatsContained.Add(BeatObj);
        Count = BeatsContained.Count;
    }

    public void RemoveBeat(Beat BeatObj)
    {
        BeatsContained.Remove(BeatObj);
        Count = BeatsContained.Count;
    }
}
