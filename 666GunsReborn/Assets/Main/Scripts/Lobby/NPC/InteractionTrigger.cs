using UnityEngine;
using System.Collections;

public class InteractionTrigger : MonoBehaviour
{
    public Transform cameraTargetPosition;      // ī�޶� �̵��� ��ġ
    public float cameraMoveSpeed = 2f;
    public GameObject player;
    public GameObject npc;
    public GameObject NpcCanvas;
    public GameObject MainCanvas;

    [SerializeField] private DialogController _dialogController;
    [SerializeField] private TopDownController _topDownController;

    private bool hasTriggered = false;

    private void Start()
    {
        _topDownController = npc.gameObject.GetComponent<TopDownController>();
        _dialogController = npc.gameObject.GetComponent<DialogController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartCoroutine(HandleInteraction());
            NpcCanvas.SetActive(true);
            MainCanvas.SetActive(false);
            _topDownController._onInteractEvent += _dialogController.DialogEvent;
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
