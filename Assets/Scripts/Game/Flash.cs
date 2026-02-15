using UnityEngine;
using UnityEngine.InputSystem;

public class Flash : MonoBehaviour
{
    [Header("연결 설정")]
    public Transform playerCamera;
    public Camera mainCam;
    public Light flashlight;
    public CanvasGroup eyeClosedOverlay; // 눈 감기 연출용 블랙 이미지

    [Header("회전 속도 설정")]
    public float cameraSpeed = 5f;
    public float flashlightSpeed = 15f;

    [Header("시야 제한")]
    public Vector2 yawLimit = new Vector2(-10, 70);   // 좌우 제한
    public Vector2 pitchLimit = new Vector2(-30, 30); // 상하 제한
    public float deadzone = 0.4f;

    private Vector2 mouseDeadzone=Vector2.zero;
    private Vector2 mouseInput;
    private float yaw;
    private float pitch;
    private bool isFlashlightOn;
    public void Start()
    {
        flashlight.enabled = false;
    }
    
    public void OnLook(InputValue value)=>mouseInput=value.Get<Vector2>();

    // 인풋 시스템에서 호출 (OnFlashlight 액션)
    public void OnFlashlight(InputValue value)
    {
       
        isFlashlightOn = value.isPressed;
        flashlight.enabled = isFlashlightOn;
    }

    // 인풋 시스템에서 호출 (OnCloseEyes 액션)
    public void OnCloseEyes(InputValue value)
    {
        
        // 눈 감기 로직 (누르고 있으면 Alpha 1, 떼면 0)
        float targetAlpha = value.isPressed ? 1f : 0f;
        StopAllCoroutines();
        StartCoroutine(FadeEyes(targetAlpha));
    }

    void Update()
    {
        Vector2 normalizedMouse = new Vector2((mouseInput.x / Screen.width)
            - 0.5f, (mouseInput.y / Screen.height) - 0.5f) * 2f;

        if (Mathf.Abs(normalizedMouse.x) > deadzone)
        {
            mouseDeadzone.x = (normalizedMouse.x > 0) ?
                (normalizedMouse.x - deadzone) : (normalizedMouse.x + deadzone);
        }
        else
        {
            mouseDeadzone.x = 0;
        }
        if (Mathf.Abs(normalizedMouse.y) > deadzone)
        {
            mouseDeadzone.y = (normalizedMouse.y > 0) ?
                (normalizedMouse.y - deadzone) : (normalizedMouse.y + deadzone);
        }
        else
        {
            mouseDeadzone.y = 0;
        }
            yaw += mouseDeadzone.x * cameraSpeed * Time.deltaTime;
        pitch-=mouseDeadzone.y * cameraSpeed * Time.deltaTime;

        yaw=Mathf.Clamp(yaw, yawLimit.x, yawLimit.y);
        pitch = Mathf.Clamp(pitch, pitchLimit.x, pitchLimit.y);

        playerCamera.rotation = Quaternion.Euler(pitch, yaw, 0);
        RotateFlashlightToMouse();
    }
    void RotateFlashlightToMouse()
    {
        Ray ray = mainCam.ScreenPointToRay(mouseInput);
        Vector3 targetPoint = ray.GetPoint(10f); // 10m 앞 지점 조준

        Quaternion targetRot = Quaternion.LookRotation(targetPoint - flashlight.transform.position);
        flashlight.transform.rotation = Quaternion.Slerp(flashlight.transform.rotation, targetRot, Time.deltaTime * flashlightSpeed);
    }
    System.Collections.IEnumerator FadeEyes(float target)
    {
        while (!Mathf.Approximately(eyeClosedOverlay.alpha, target))
        {
            eyeClosedOverlay.alpha = Mathf.MoveTowards(eyeClosedOverlay.alpha, target, Time.deltaTime * 5f);
            yield return null;
        }
    }
}
