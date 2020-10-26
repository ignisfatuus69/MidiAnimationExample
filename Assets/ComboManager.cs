using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class OnCombAdded : UnityEvent { };

[System.Serializable]
public class OnComboReset : UnityEvent { };
public class ComboManager : MonoBehaviour
{
    public BeatSpawner BeatSpawnerObj;
    public Resource ComboResource;


    // Start is called before the first frame update
    void Start()
    {
        BeatSpawnerObj.EVT_OnBeatPooled.AddListener(ComboEvaluation);
    }

   private void ComboEvaluation(Beat BeatInteracted)
    {
        if (BeatInteracted.Status == BeatState.Early)
        {
            ComboResource.ReduceValue(ComboResource.Value);
        }
        if (BeatInteracted.Status == BeatState.Okay)
        {
            ComboResource.AddValue(1);
        }
        if (BeatInteracted.Status == BeatState.Perfect)
        {
            ComboResource.AddValue(1);
        }
        if (BeatInteracted.Status == BeatState.Late)
        {
            ComboResource.ReduceValue(ComboResource.Value);
        }
        if (BeatInteracted.Status == BeatState.End)
        {
            ComboResource.ReduceValue(ComboResource.Value);
        }
    }

 

   

  
}
