using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EVENT_Beats : MonoBehaviour
{
    public SpriteRenderer BeatSpriteRenderer;
    public Collider BeatCollider;
    public Animator BeatAnimator;
    public Beat BeatObj;
    // Start is called before the first frame update
    void Start()
    {
        BeatObj.EVT_OnEarlyState.AddListener(MissedBeats);
    }

    private void OnEnable()
    {
        BeatCollider.enabled = true;
        BeatSpriteRenderer.color = Color.white;
    }

    void MissedBeats()
    {

        BeatCollider.enabled = false;
        BeatAnimator.SetTrigger("Missed");
        BeatSpriteRenderer.color = new Color(100, 100, 100, 135);

    }
    
}
