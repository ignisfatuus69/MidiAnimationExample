using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using System.IO;

public class Sequencer : MonoBehaviour
{
    // private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";
    public float OffSetBeatTime;
    public string JsonBeatStamp;
    public string JsonFileName;
    public BeatSpawner BeatSpawnerObj;
    public PlayableDirector PlayableDirectorObj;
    private TimeStampContainer loadedTimeStamp = new TimeStampContainer();
    private int index = 0;

    public GameObject TestLang;
    
    private void Awake()
    {
        GetTimeStampFromJson();
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
