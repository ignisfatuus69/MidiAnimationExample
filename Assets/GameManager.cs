using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameDifficulty { Easy,Medium,Hard};
public class GameManager : MonoBehaviour
{
    public float EasySpeed, MediumSpeed, HardSpeed;
    public BeatSpawner BeatSpawnerObj;
    public GameDifficulty GameDifficultyOptions;
    // Start is called before the first frame update
    void Start()
    {
        SetDifficultyParameters();
    }



    private void SetDifficultyParameters()
    {
        //Set Points Bonuses for Harder Difficulties ?
        if (GameDifficultyOptions == GameDifficulty.Easy)
        {
            BeatSpawnerObj.BeatAnimationSpeed = EasySpeed;
            return;
        }
        if (GameDifficultyOptions == GameDifficulty.Medium)
        {
            BeatSpawnerObj.BeatAnimationSpeed = MediumSpeed;
            return;
        }
        if (GameDifficultyOptions == GameDifficulty.Hard)
        {
            BeatSpawnerObj.BeatAnimationSpeed = HardSpeed;
            return;
        }
    }

    private void FinishSong()
    {

    }
}
