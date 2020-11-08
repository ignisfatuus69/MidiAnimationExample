using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Events are based on the state
[System.Serializable]
public class OnOkayState : UnityEvent { };
[System.Serializable]
public class OnEarlyState : UnityEvent { };
[System.Serializable]
public class OnPerfectState : UnityEvent { };
[System.Serializable]
public class OnLateState : UnityEvent { };
[System.Serializable]
public class OnEndState : UnityEvent <Beat> { };

public enum BeatState { Early, Okay, Perfect,Late,End };
public class Beat : MonoBehaviour
{
    public SpriteRenderer BeatSpriteRenderer;
    public SpriteRenderer RingSpriteRenderer;
    public Transform RingTransform;

    public Sequencer SequencerRef;
    public Animator BeatAnimator;
    public Animation SpriteAnimation;

    public OnEarlyState EVT_OnEarlyState;
    public OnOkayState EVT_OnOkayState;
    public OnPerfectState EVT_OnPerfectState;
    public OnLateState EVT_OnLateState;
    public OnEndState EVT_OnEndState;
    public SphereCollider BeatCollider;

    public bool IsInteractable = false;
    public BeatState Status { get;  set; }
    public int ScoreValue { get; private set; }
    public double CurrentTimeStamp;
    public int Index = 0;
    // Start is called before the first frame update
    void Start()
    {
        ScoreValue = 0;

    }

    private void OnEnable()
    {

        ScoreValue = 0;
        StartCoroutine(DelayAnimationSetters());
        // need to start late
       
    }

    private void OnDisable()
    {
        BeatAnimator.playbackTime = 0;

        this.BeatCollider.center = new Vector3(0, 0, 0);
        IsInteractable = false;
    

        RingTransform.localScale = new Vector3(2,2,2);
        BeatSpriteRenderer.color = new Color(0.25f, 0.25f, 0.25f, 0.25f);
        RingSpriteRenderer.color = new Color(0.25f, 0.25f, 0.25f, 0.25f);
    }

    private void ActivateEarlyState()
    {
        ScoreValue = 0;
      //  EVT_OnEarlyState.Invoke();
        Status = BeatState.Early;
    }

    private void ActivateOkayState()
    {
        ScoreValue = 100;
      //  EVT_OnOkayState.Invoke();
        Status = BeatState.Okay;
    }

    private void ActivatePerfectState()
    {
        ScoreValue = 300;
    //    EVT_OnPerfectState.Invoke();
        
        Status = BeatState.Perfect;
    }

    private void ActivateLateState()
    {
        ScoreValue = 0;
     //   EVT_OnLateState.Invoke();
        Status = BeatState.Late;
    }

    private void ActivateEndState()
    {
        ScoreValue = 0;
        BeatAnimator.enabled = false;
        Status = BeatState.Late;
        EVT_OnEndState.Invoke(this);

    }

    private void ActivateStates()
    {
        // Early State
        if (SequencerRef.PlayableDirectorObj.time > (this.CurrentTimeStamp + (SequencerRef.OffSetBeatTime / 3.25f))
            && SequencerRef.PlayableDirectorObj.time < (this.CurrentTimeStamp + (SequencerRef.OffSetBeatTime / 2.5f)))
        {

            ActivateEarlyState();
        }
        // Okay State
        if (SequencerRef.PlayableDirectorObj.time > (this.CurrentTimeStamp + (SequencerRef.OffSetBeatTime / 2.5f))
            && SequencerRef.PlayableDirectorObj.time < (this.CurrentTimeStamp + (SequencerRef.OffSetBeatTime / 1.5f)))
        {
            //
            ActivateOkayState();
        }
        // Perfect State
        if (SequencerRef.PlayableDirectorObj.time > (this.CurrentTimeStamp + (SequencerRef.OffSetBeatTime / 1.5f))
            && SequencerRef.PlayableDirectorObj.time < this.CurrentTimeStamp + SequencerRef.OffSetBeatTime)
        {
            //
            ActivatePerfectState();
        }
        // Late/End State
        if (SequencerRef.PlayableDirectorObj.time > this.CurrentTimeStamp + SequencerRef.OffSetBeatTime)
        {

            ActivateEndState();
        }

    }

    private void EnableBasedOnTimestamp()
    {
       

        if (this.Index == 0)
        {
            this.IsInteractable = true;
            this.BeatCollider.center = new Vector3(0, 0, 1);
        }
            // second beat until the last beat interactable is being managed

        if (this.Index > 0 && this.Index
           <= (SequencerRef.BeatSequencerInfo.TimeStampsNumbers.Count-1))
        {
            // if the current time is greater than the last beat has finished, make the current beat interactable
            if (SequencerRef.CurrentInteractedBeatIndex == this.Index)
            //if (SequencerRef.PlayableDirectorObj.time
            //    >= (SequencerRef.loadedTimeStamp.TimeStampsNumbers[this.Index - 1] + SequencerRef.OffSetBeatTime) )
            {
                this.IsInteractable = true;
                this.BeatCollider.center = new Vector3(0, 0, 1);
                return;
            }

            // if the beat before yours isnt active anymore
            //else
            //{
            //    this.BeatCollider.center = new Vector3(0, 0, 0);
            //    this.IsInteractable = false;
               
            //}
        }
    }

    //Copy Pasta Galore
    IEnumerator ScaleBeatBasedOnTime(Transform ObjectToScale, Vector3 TargetScale, float TimeToScale)
    {
        Vector3 CurrentScale = ObjectToScale.transform.localScale;
        float time = 0f;
        while (time < 1)
        {
            time += Time.deltaTime / TimeToScale;
            RingTransform.localScale = Vector3.Lerp(CurrentScale, TargetScale, time);
            yield return null;
        }
    }
    IEnumerator SetBeatColorBasedOnTime(SpriteRenderer SpriteRendererRef, Vector4 TargetColor, float TimeToScale)
    {
        Color SpriteColor = SpriteRendererRef.color;
        float time = 0f;
        while (time < 1)
        {
            time += Time.deltaTime / TimeToScale;
            BeatSpriteRenderer.color = Vector4.Lerp(SpriteColor, TargetColor, time);
            yield return null;
        }
    }

    IEnumerator SetRingColorBasedOnTime(SpriteRenderer SpriteRendererRef, Vector4 TargetColor, float TimeToScale)
    {
        Color SpriteColor = SpriteRendererRef.color;
        float time = 0f;
        while (time < 1)
        {
            time += Time.deltaTime / TimeToScale;
            RingSpriteRenderer.color = Vector4.Lerp(SpriteColor, TargetColor, time);
            yield return null;
        }
    }

    IEnumerator DelayAnimationSetters()
    {
        yield return new WaitForSeconds(0.05f);
        StartCoroutine(ScaleBeatBasedOnTime(RingTransform, new Vector3(0.87f, 0.87f, 0.87f), SequencerRef.OffSetBeatTime - (SequencerRef.OffSetBeatTime/4)));
        StartCoroutine(SetBeatColorBasedOnTime(BeatSpriteRenderer, new Vector4(1, 1, 1, 1), SequencerRef.OffSetBeatTime * 1.25f));
        StartCoroutine(SetRingColorBasedOnTime(RingSpriteRenderer, new Vector4(1, 1, 1, 1), SequencerRef.OffSetBeatTime * 1.25f));
    }

    private void Update()
    {
        EnableBasedOnTimestamp();
        ActivateStates();

    }




}
