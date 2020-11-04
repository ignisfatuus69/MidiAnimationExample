using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameDifficulty { Easy,Medium,Hard};
public class GameManager : MonoBehaviour
{
    public float EasySpeed, MediumSpeed, HardSpeed;
    public BeatSpawner BeatSpawnerObj;
    public GameDifficulty GameDifficultyOptions;
    public Sequencer SequencerRef;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void EnableSpawner()
    {
        if (SequencerRef.PlayableDirectorObj.time - 1 >= SequencerRef.loadedTimeStamp.TimeStampsNumbers[0])
        {
            BeatSpawnerObj.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        EnableSpawner();
    }
}
