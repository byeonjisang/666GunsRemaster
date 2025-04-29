using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectStage : MonoBehaviour
{
    public void StageSelect(int stageIndex)
    {
        StageManager.Instance.StartStage(stageIndex);
    }
}
