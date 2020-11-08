using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using System.IO;
using UnityEngine.Events;

[System.Serializable]
public class OnBeatTimedUp : UnityEvent<Beat> { };
public class Sequencer : MonoBehaviour
{
    // private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";
    public float OffSetBeatTime;
    public string JsonBeatStamp { private set; get; }
    public string JsonFileName;
    public BeatSpawner BeatSpawnerObj;
    public PlayableDirector PlayableDirectorObj;
    public BeatSequencerInfo BeatSequencerInfo { private set; get; } = new BeatSequencerInfo();

    public int index {  set; get; } = 0;

    public OnBeatTimedUp EVT_OnBeatTimedUp;
    public int CurrentInteractedBeatIndex { private set; get; } = 0;

    private void Awake()
    {
        GetTimeStampFromJson();
    }

    private void Start()
    {
        BeatSpawnerObj.EVT_OnBeatSpawned.AddListener(SetBeatTimeStamp);
        BeatSpawnerObj.EVT_OnBeatPooled.AddListener(CountBeatBasedOnSpawner);

    }
    private void GetTimeStampFromJson()
    {
        JsonBeatStamp = File.ReadAllText(Application.dataPath + "/" + JsonFileName + ".json");
        BeatSequencerInfo = JsonUtility.FromJson<BeatSequencerInfo>(JsonBeatStamp);
        Debug.Log(JsonBeatStamp);
    }

    private void Update()
    {

        //We can have an offset time for the beats to spawn
        if (PlayableDirectorObj.time >= BeatSequencerInfo.TimeStampsNumbers[index])
        {
            BeatSpawnerObj.SpawnObjects();
            index += 1;
        }
    }


    private void SetBeatTimeStamp(Beat BeatSpawned)
    {
        BeatSpawned.Index = this.index;
        BeatSpawned.CurrentTimeStamp = BeatSequencerInfo.TimeStampsNumbers[index];

    }

    private void CountBeatBasedOnSpawner(Beat BeatHit)
    {
        //Last Beat that we hit is just -1 away
        CurrentInteractedBeatIndex += 1;
    }

}
