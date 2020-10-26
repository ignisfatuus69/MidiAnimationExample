using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatContainer :MonoBehaviour
{
    public Vector3 Position;
    public List<GameObject> BeatsContained = new List<GameObject>();
    public int Count = 0;
    public void AddBeat(GameObject obj)
    {
        BeatsContained.Add(obj);
    }

    public void RemoveBeat(GameObject obj)
    {
        BeatsContained.Remove(obj);
    }
}
