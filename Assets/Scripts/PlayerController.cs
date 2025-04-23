using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 5f;
    public float gravity = 9.81f;
    public Transform cameraHolder;
    public float mouseSensitivity = 2f;

    private CharacterController controller;
    private Vector2 rotation;
    private bool isRunning = false;
    private Vector3 velocity;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // カメラ回転
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotation.x -= mouseY;
        rotation.x = Mathf.Clamp(rotation.x, -90f, 90f);
        rotation.y += mouseX;

        cameraHolder.localRotation = Quaternion.Euler(rotation.x, 0, 0);
        transform.rotation = Quaternion.Euler(0, rotation.y, 0);

        // ダッシュ制御
        if (Input.GetKeyDown(KeyCode.LeftControl)) isRunning = true;
        if (Input.GetKeyUp(KeyCode.W)) isRunning = false;
        float speed = isRunning ? runSpeed : walkSpeed;

        // 移動入力
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = transform.right * h + transform.forward * v;

        // ジャンプ＆重力
        if (controller.isGrounded)
        {
            velocity.y = -2f; // 地面にくっつける
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = jumpForce;
            }
        }

        velocity.y -= gravity * Time.deltaTime;
        controller.Move((move * speed + velocity) * Time.deltaTime);
    }
}
