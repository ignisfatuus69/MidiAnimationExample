using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using System.IO;

[System.Serializable]
public class TimeStampContainer
{
    public List<double> TimeStampsNumbers = new List<double>();
}
public class BeatCounter : MonoBehaviour
{
   

    public TimeStampContainer TimeStampToStore = new TimeStampContainer();
    public int TotalBeatCount;
    public string JsonFileName;
    public PlayableDirector PlayableDirectorObj;


    public void CountBeat()
    {
        TimeStampToStore.TimeStampsNumbers.Add(PlayableDirectorObj.time);
        if (TimeStampToStore.TimeStampsNumbers.Count>= TotalBeatCount)
        {
            Debug.Log(TimeStampToStore.TimeStampsNumbers.Count);

            string json = JsonUtility.ToJson(TimeStampToStore);

            File.WriteAllText(Application.dataPath + "/" + JsonFileName + ".json", json);
            Debug.Log(json);
        }

    }



  
}
