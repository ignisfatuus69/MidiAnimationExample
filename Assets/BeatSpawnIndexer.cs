using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSpawnIndexer : MonoBehaviour
{
    public int IndexedBeatsCount;
    public BeatVariableSetter BeatVariableSetterObj;
    public int[] IndexVariable;
    public bool HasDouble;
   // public BeatSpawnIndexer BeatSpawnIndexerDouble;
    public void SetIndexVariable()
    {
       // Only for singles
       if (IndexVariable.Length<=1)
        {
            BeatVariableSetterObj.BeatSequencerInfo.SpawnPointIndex.Add(this.IndexVariable[0]);
            IndexedBeatsCount += 1;
            Debug.Log(this.IndexVariable[0]);
            return;
        }
       else
        {
            EvaluateDoubleIndexer();
        }

        
    }

    public void EvaluateDoubleIndexer()
    {
        //This solution only works if there's 2
        //Even Numbers
        if (IndexedBeatsCount%2==0)
        {
            BeatVariableSetterObj.BeatSequencerInfo.SpawnPointIndex.Add(this.IndexVariable[0]);
            IndexedBeatsCount += 1;
            Debug.Log(this.IndexVariable[0]);
            return;
        }
        //Odd numbers
        else if (IndexedBeatsCount%2==1)
        {
            BeatVariableSetterObj.BeatSequencerInfo.SpawnPointIndex.Add(this.IndexVariable[1]);
            IndexedBeatsCount += 1;
            Debug.Log(this.IndexVariable[1]);
            return;
        }
    }

    
}
