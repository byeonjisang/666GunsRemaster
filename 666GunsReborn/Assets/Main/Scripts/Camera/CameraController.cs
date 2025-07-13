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
    public void StartCameraMoveToPlayer(Vector3 pos, float speed)
    {
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        // Follow 해제
        virtualCamera.Follow = player.transform;

        moveCoroutine = StartCoroutine(MoveCameraCoroutine(pos, speed));
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

    public bool IsCameraAtTarget()
    {
        return !isMove;
    }
}
