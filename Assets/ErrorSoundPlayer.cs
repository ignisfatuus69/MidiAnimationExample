using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorSoundPlayer : MonoBehaviour
{
    public BeatSpawner BeatSpawnerObj;
    public AudioSource ErrorSoundSource;
    public Sequencer SequencerObj;
    // Start is called before the first frame update
    void Start()
    {
        BeatSpawnerObj.EVT_OnBeatPooled.AddListener(PlayErrorSoundOnMiss);
    }

    void PlayErrorSoundOnMiss(Beat BeatObjReference)
    {
        if (BeatObjReference.Status == BeatState.Early || BeatObjReference.Status == BeatState.Late)
        {
            StartCoroutine(WaitToPlay());
        }
        else return;
    }

    IEnumerator WaitToPlay()
    {
        yield return new WaitForSeconds(SequencerObj.OffSetBeatTime / 4);
        ErrorSoundSource.Play();
    }
}
