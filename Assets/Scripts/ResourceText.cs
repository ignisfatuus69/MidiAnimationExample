using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class ResourceText : MonoBehaviour
{
    public Text TextObj;
    public Resource ResourceObj;
    public string HeaderText;
    // Start is called before the first frame update
    void Start()
    {
        ResourceObj.EVT_OnResourceInitialized.AddListener(UpdateText);
        ResourceObj.EVT_OnResourceAdded.AddListener(UpdateText);
        ResourceObj.EVT_OnResourceSubtracted.AddListener(UpdateText);
    }

    void UpdateText()
    {
        TextObj.text =  ResourceObj.Value + HeaderText;
    }

    
}
