using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnBeatInteraction : UnityEvent<Beat> { };
[System.Serializable]
public class OnBeatEvaluating : UnityEvent<Beat> { };
[System.Serializable]
public class OnBeatEvaluated : UnityEvent<Beat> { };

public class BeatInteractor : MonoBehaviour
{
    private bool CanInteract = true;
    public OnBeatInteraction EVT_OnBeatInteraction;
    public OnBeatEvaluating EVT_OnBeatEvaluating;
    public OnBeatEvaluated EVT_OnBeatEvaluated;
    // Start is called before the first frame update
  

    protected virtual void EvaluateBeatStates(Beat BeatToEvaluate)
    {
        if (CanInteract == false) return;
        EVT_OnBeatInteraction.Invoke(BeatToEvaluate);
        if (BeatToEvaluate.Status == BeatState.Early)
        {
            EVT_OnBeatEvaluating.Invoke(BeatToEvaluate);
            Debug.Log("Early");
            EVT_OnBeatEvaluated.Invoke(BeatToEvaluate);
        }

        else if (BeatToEvaluate.Status == BeatState.Okay)
        {
            EVT_OnBeatEvaluating.Invoke(BeatToEvaluate);
            Debug.Log("Okay");
            EVT_OnBeatEvaluated.Invoke(BeatToEvaluate);

        }
        else if (BeatToEvaluate.Status == BeatState.Perfect)
        {
            EVT_OnBeatEvaluating.Invoke(BeatToEvaluate);
            Debug.Log("Perfect");
            EVT_OnBeatEvaluated.Invoke(BeatToEvaluate);
        }
        StartCoroutine(EnableInteraction());
    }

    IEnumerator EnableInteraction()
    {
        yield return new WaitForSeconds(0.01f);
        CanInteract = true;
    }
}
