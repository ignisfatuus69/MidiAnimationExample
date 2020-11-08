using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using System.IO;
using UnityEngine.Events;


[System.Serializable]
public class BeatSequencerInfo
{
    public List<double> TimeStampsNumbers = new List<double>();
    public List<int> SpawnPointIndex = new List<int>();
}

public class BeatVariableSetter : MonoBehaviour
{
   

    public BeatSequencerInfo BeatSequencerInfo = new BeatSequencerInfo();
    public int TotalBeatCount;
    public string JsonFileName;
    public PlayableDirector PlayableDirectorObj;


    public void CountBeat()
    {
        Debug.Log("parechong nagbibilang");
        BeatSequencerInfo.TimeStampsNumbers.Add(PlayableDirectorObj.time);
   
        if (BeatSequencerInfo.TimeStampsNumbers.Count>= TotalBeatCount)
        {
           
            Debug.Log(BeatSequencerInfo.TimeStampsNumbers.Count);

            StartCoroutine(PareChong());
        }

    }

    IEnumerator PareChong()
    {
        yield return new WaitForSeconds(5);
        string json = JsonUtility.ToJson(BeatSequencerInfo);

        File.WriteAllText(Application.dataPath + "/" + JsonFileName + ".json", json);
        Debug.Log(json);
        Debug.Log("tapos na");
    }



  
}
