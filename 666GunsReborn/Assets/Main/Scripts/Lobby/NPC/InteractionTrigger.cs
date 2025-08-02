using UnityEngine;
using System.Collections;

public class InteractionTrigger : MonoBehaviour
{
    public Transform cameraTargetPosition;      // ī�޶� �̵��� ��ġ
    public float cameraMoveSpeed = 2f;
    public GameObject player;
    public GameObject NpcCanvas;
    public GameObject MainCanvas;

    [TextArea]
    public string dialogueText = "�⺻ ����Դϴ�.";

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartCoroutine(HandleInteraction());
            NpcCanvas.SetActive(true);
            MainCanvas.SetActive(false);
        }
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        hasTriggered = false;
    //        StartCoroutine(HandleInteractionToPlayer());
    //    }
    //}

    private IEnumerator HandleInteraction()
    {
        if (CameraController.Instance != null)
        {
            Debug.Log("NPC ��ȣ�ۿ��");
            CameraController.Instance.StartCameraMove(cameraTargetPosition.position, cameraMoveSpeed);
        }

         hasTriggered = false;

        yield return new WaitUntil(() => CameraController.Instance.IsCameraAtTarget());

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
