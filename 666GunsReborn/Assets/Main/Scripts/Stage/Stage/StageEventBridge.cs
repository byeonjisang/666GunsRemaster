using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEventBridge : MonoBehaviour
{
    public void CallCompleteStage()
    {
        if(StageManager.Instance != null)
        {
            StageManager.Instance.StageClear();
        }
    }
}
