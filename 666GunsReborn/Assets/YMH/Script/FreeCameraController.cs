using UnityEngine;

public class FreeCameraController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float lookSensitivity = 2f;
    public float zoomSensitivity = 2f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 고정
        Cursor.visible = false;
    }

    void Update()
    {
        // 마우스 회전
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        Debug.Log($"Mouse X: {mouseX}, Mouse Y: {mouseY}");
        rotationX -= mouseY;
        rotationY += mouseX;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // 위아래 제한

        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);

        // 이동 (WASD + QE)
        float moveX = Input.GetAxis("Horizontal"); // A/D
        float moveZ = Input.GetAxis("Vertical");   // W/S
        float moveY = 0f;

        if (Input.GetKey(KeyCode.E)) moveY += 1f;   // 올라가기
        if (Input.GetKey(KeyCode.Q)) moveY -= 1f;   // 내려가기

        Vector3 move = (transform.right * moveX + transform.up * moveY + transform.forward * moveZ) * moveSpeed * Time.deltaTime;
        transform.position += move;

        // 마우스 휠로 속도 조절 (선택)
        if (Input.mouseScrollDelta.y != 0)
        {
            moveSpeed += Input.mouseScrollDelta.y * zoomSensitivity;
            moveSpeed = Mathf.Clamp(moveSpeed, 1f, 100f);
        }
    }
}