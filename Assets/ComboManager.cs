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

    public Resource ComboResource;
    public BeatManager BeatManagerObj;

    // Start is called before the first frame update
    void Start()
    {
        BeatManagerObj.EVT_OnEarlyBeat.AddListener(ResetCombo);
        BeatManagerObj.EVT_OnEarlyBeat.AddListener(ResetCombo);
        BeatManagerObj.EVT_OnOkayBeat.AddListener(AddCombo);
        BeatManagerObj.EVT_OnPerfectBeat.AddListener(AddCombo);
    }

    void AddCombo()
    {
        ComboResource.AddValue(1);
    }

    void ResetCombo()
    {
        ComboResource.ReduceValue(ComboResource.Value);
    }

 

   

  
}
