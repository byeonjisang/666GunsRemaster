using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEventBridge : MonoBehaviour
{
    public void CallCompleteStage()
    {
        if(StageManagers.Instance != null)
        {
            StageManagers.Instance.StageClear();
        }
    }
}
