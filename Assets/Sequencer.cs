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
    public TimeStampContainer loadedTimeStamp { private set; get; } = new TimeStampContainer();
    public int index {  set; get; } = 0;

    public OnBeatTimedUp EVT_OnBeatTimedUp;

    private void Awake()
    {
        GetTimeStampFromJson();
    }

    private void Start()
    {
        BeatSpawnerObj.EVT_OnBeatSpawned.AddListener(SetBeatTimeStamp);
        BeatSpawnerObj.EVT_OnBeatSpawned.AddListener(EnableBeatBasedOnTime);


    }
    private void GetTimeStampFromJson()
    {
        JsonBeatStamp = File.ReadAllText(Application.dataPath + "/" + JsonFileName + ".json");
        loadedTimeStamp = JsonUtility.FromJson<TimeStampContainer>(JsonBeatStamp);
        Debug.Log(JsonBeatStamp);
    }

    private void Update()
    {

        //We can have an offset time for the beats to spawn
        if (PlayableDirectorObj.time >= loadedTimeStamp.TimeStampsNumbers[index])
        {
            BeatSpawnerObj.SpawnObjects();
            index += 1;
        }
    }


    private void SetBeatTimeStamp(Beat BeatSpawned)
    {
        BeatSpawned.Index = this.index;
        BeatSpawned.CurrentTimeStamp = loadedTimeStamp.TimeStampsNumbers[index];

    }

    private void EnableBeatBasedOnTime(Beat BeatSpawned)
    {
        //if (this.index == 0)
        //{
        //    BeatSpawned.IsInteractable = true;
        //    return;
        //}
    }
    private void CheckBeatTimeStamps()
    {
    
    }
    //void SetScaling()
    //{
    //    if (Input.GetKey(KeyCode.E))
    //    {
    //        TestLang.transform.localScale =  Vector3.Lerp(TestLang.transform.localScale, new Vector3(0.25f, 0.25f, 0.25f), 1*Time.deltaTime);

    //        //Scale the objects by lerping from offset time to actual loadedTimeStampTime
    //    }
    //}

    //IEnumerator ScaleBySeconds(float Seconds)
    //{

    //}
}
