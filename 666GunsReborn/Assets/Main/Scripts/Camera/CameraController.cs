using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraController : Singleton<CameraController>
{
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin noise;

    private Coroutine shakeCoroutine;
    private Coroutine moveCoroutine;

    private Vector3 targetPos;
    private float moveSpeed;
    private bool isMove = false;

    private Transform mainCameraTransform;
    public GameObject player;

    public float a;
    public float b;

    protected override void Awake()
    {
        base.Awake();
        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        // 메인 카메라 캐시
        mainCameraTransform = Camera.main.transform;
    }

    void Start()
    {
        noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float duration, float amplitude = 2f, float frequency = 2f)
    {
        if (shakeCoroutine != null)
            StopCoroutine(shakeCoroutine);

        shakeCoroutine = StartCoroutine(ShakeCoroutine(duration, amplitude, frequency));
    }

    private IEnumerator ShakeCoroutine(float duration, float amplitude, float frequency)
    {
        noise.m_AmplitudeGain = a;
        noise.m_FrequencyGain = b;

        yield return new WaitForSeconds(duration);

        noise.m_AmplitudeGain = 0f;
        noise.m_FrequencyGain = 0f;
        shakeCoroutine = null;
    }

    public void StartCameraMove(Vector3 pos, float speed)
    {
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        // Follow 해제
        virtualCamera.Follow = null;

        moveCoroutine = StartCoroutine(MoveCameraCoroutine(pos, speed));
    }
    public void StartCameraMoveToPlayer(float speed)
    {
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        // 카메라가 플레이어를 보는 위치를 수동으로 계산
        Vector3 zoomOutOffset = new Vector3(0, 10, -10); // 필요에 따라 수정 가능
        Vector3 targetPos = player.transform.position + zoomOutOffset;

        virtualCamera.Follow = null; // Follow 잠시 해제
        moveCoroutine = StartCoroutine(MoveCameraToPlayerCoroutine(targetPos, speed));
    }


    private IEnumerator MoveCameraCoroutine(Vector3 pos, float speed)
    {
        isMove = true;

        while (Vector3.Distance(virtualCamera.transform.position, pos) > 0.5f)
        {
            virtualCamera.transform.position = Vector3.Lerp(virtualCamera.transform.position, pos, Time.deltaTime * speed);
            yield return null;
        }

        virtualCamera.transform.position = pos;
        isMove = false;
        moveCoroutine = null;
    }

    private IEnumerator MoveCameraToPlayerCoroutine(Vector3 targetPos, float speed)
    {
        isMove = true;

        while (Vector3.Distance(virtualCamera.transform.position, targetPos) > 0.5f)
        {
            virtualCamera.transform.position = Vector3.Lerp(virtualCamera.transform.position, targetPos, Time.deltaTime * speed);
            yield return null;
        }

        virtualCamera.transform.position = targetPos;
        isMove = false;
        moveCoroutine = null;

        // 보간 끝나고 Follow 다시 설정
        virtualCamera.Follow = player.transform;
    }


    public bool IsCameraAtTarget()
    {
        return !isMove;
    }
}
