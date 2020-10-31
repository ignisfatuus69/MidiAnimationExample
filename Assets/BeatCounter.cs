using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using System.IO;
public class TimeStamps
{
    public List<double> TimeStampsNumbers = new List<double>();
}
public class BeatCounter : MonoBehaviour
{
   

    public TimeStamps TS = new TimeStamps();
    public List<double> BTS = new List<double>();
    public int TotalBeatCount;
    public string JsonFileName;
    public PlayableDirector PD;

    public string jsonBeatStamp;

    // Start is called before the first frame update
    void Start()
    {
        LoadJson();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CountBeat()
    {
        //   Debug.Log(PD.time);

        //   BeatTimeStamps.Add(PD.time);
        TS.TimeStampsNumbers.Add(PD.time);
        if (TS.TimeStampsNumbers.Count>= TotalBeatCount)
        {
            Debug.Log(TS.TimeStampsNumbers.Count);

            string json = JsonUtility.ToJson(TS);

            File.WriteAllText(Application.dataPath + "/" + JsonFileName + ".json", json);
            Debug.Log(json);
        }

    }

    public void LoadJson()
    {
        jsonBeatStamp = File.ReadAllText(Application.dataPath + "/" + JsonFileName + ".json");
        TimeStamps LoadedTimeStamps = JsonUtility.FromJson<TimeStamps>(jsonBeatStamp);
        Debug.Log(jsonBeatStamp);
    }

  
}
