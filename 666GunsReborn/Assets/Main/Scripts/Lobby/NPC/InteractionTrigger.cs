using UnityEngine;
using System.Collections;

public class InteractionTrigger : MonoBehaviour
{
    public Transform cameraTargetPosition;      // 카메라가 이동할 위치
    public float cameraMoveSpeed = 2f;
    public GameObject player;
    public GameObject NpcCanvas;
    public GameObject MainCanvas;

    [TextArea]
    public string dialogueText = "기본 대사입니다.";

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
            Debug.Log("NPC 상호작용됨");
            CameraController.Instance.StartCameraMove(cameraTargetPosition.position, cameraMoveSpeed);
        }

         hasTriggered = false;

        yield return new WaitUntil(() => CameraController.Instance.IsCameraAtTarget());

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
