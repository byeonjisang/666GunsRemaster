using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomOutAndExit : MonoBehaviour
{
    public float cameraMoveSpeed = 2f;
    public GameObject NpcCanvas;
    public GameObject MainCanvas;

    public void ExitNpcDialog()
    {
        StartCoroutine(HandleInteractionToPlayer());
        NpcCanvas.SetActive(false);
        MainCanvas.SetActive(true);
    }

    private IEnumerator HandleInteractionToPlayer()
    {
        if (CameraController.Instance != null)
        {
            Debug.Log("NPC 상호작용 해제됨");
            CameraController.Instance.StartCameraMoveToPlayer(cameraMoveSpeed); // 파라미터 변경
        }

        yield return new WaitUntil(() => CameraController.Instance.IsCameraAtTarget());
    }
}
