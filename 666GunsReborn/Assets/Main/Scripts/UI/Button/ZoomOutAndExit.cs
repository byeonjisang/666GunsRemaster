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
            Debug.Log("NPC ��ȣ�ۿ� ������");
            CameraController.Instance.StartCameraMoveToPlayer(cameraMoveSpeed); // �Ķ���� ����
        }

        yield return new WaitUntil(() => CameraController.Instance.IsCameraAtTarget());
    }
}
