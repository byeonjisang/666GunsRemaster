using UnityEngine;
using System.Collections;

public class InteractionTrigger : MonoBehaviour
{
    public Transform cameraTargetPosition;      // ī�޶� �̵��� ��ġ
    public float cameraMoveSpeed = 2f;
    public GameObject player;

    [TextArea]
    public string dialogueText = "�⺻ ����Դϴ�.";

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartCoroutine(HandleInteraction());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasTriggered = false;
            StartCoroutine(HandleInteraction());
        }
    }

    private IEnumerator HandleInteraction()
    {
        if (CameraController.Instance != null)
        {
            Debug.Log("NPC ��ȣ�ۿ��");
            CameraController.Instance.StartCameraMove(cameraTargetPosition.position, cameraMoveSpeed);
        }

        yield return new WaitUntil(() => CameraController.Instance.IsCameraAtTarget());

    }

    private IEnumerator HandleInteractionToPlayer()
    {
        if (CameraController.Instance != null)
        {
            Debug.Log("NPC ��ȣ�ۿ� ������");
            CameraController.Instance.StartCameraMoveToPlayer(player.transform.position, cameraMoveSpeed);
        }

        yield return new WaitUntil(() => CameraController.Instance.IsCameraAtTarget());

    }
}
