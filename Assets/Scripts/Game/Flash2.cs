using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;


public class Flash2 : MonoBehaviour
{
    [Header("연결 설정")]
    [SerializeField] private Transform playerCamera;
    [SerializeField] private Camera mainCam;
    [SerializeField] private Light flashlight;
    [SerializeField] private CanvasGroup eyeClosedOverlay; // 눈 감기 연출용 블랙 이미지
    private CharacterController cc;
    private bool isFlashlightOn;
    private Vector2 moveInput;
    private Vector3 velocity;

    private float gravity = -9.8f;
    private float moveSpeed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cc=GetComponent<CharacterController>();
        isFlashlightOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerMove();
    }
    public void OnFlashlight(InputValue value)
    {

        isFlashlightOn = value.isPressed;
        flashlight.enabled = isFlashlightOn;
    }
    public void OnMove(InputValue value)
    {
        moveInput=value.Get<Vector2>();
    }
    public void playerMove()
    {
        if (cc.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector3 move = playerCamera.transform.right * moveInput.x + playerCamera.transform.forward * moveInput.y;
        move=move.normalized;
        cc.Move(move * moveSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }
}
