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
    //public Vector2 pitchLimit = new Vector2(-30, 30); // 상하 제한
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
        if (PlayerState.ps.pState == PlayerState.State.Move ||
            PlayerState.ps.pState == PlayerState.State.Attacked ||
            PlayerState.ps.pState == PlayerState.State.GameOver ||
            PlayerState.ps.pState == PlayerState.State.Sleep)
        {
            isFlashlightOn = false;
            flashlight.enabled = false;
            return; // 여기서 함수를 종료해버리면 아래쪽의 이동 로직이 실행 안 됨
        }
        isFlashlightOn = value.isPressed;
        flashlight.enabled = isFlashlightOn;
        if (isFlashlightOn)
        {
            PlayerState.ps.pFlash();
        }
        else
        {
            PlayerState.ps.pIdle();
        }
    }

    // 인풋 시스템에서 호출 (OnCloseEyes 액션)
    public void OnCloseEyes(InputValue value)
    {
        
        // 눈 감기 로직 (누르고 있으면 Alpha 1, 떼면 0)
        float targetAlpha = value.isPressed ? 1f : 0f;
        StopAllCoroutines();
        if (targetAlpha == 1f)
        {
            PlayerState.ps.pSleep();
        }
        else if (targetAlpha == 0f) { 
            PlayerState.ps.pIdle();
        }
        StartCoroutine(FadeEyes(targetAlpha));
    }

    void Update()
    {
        if (PlayerState.ps.pState == PlayerState.State.Move||
            PlayerState.ps.pState == PlayerState.State.Attacked||
            PlayerState.ps.pState == PlayerState.State.GameOver||
            PlayerState.ps.pState == PlayerState.State.Sleep
            )
        {
            return; // 여기서 함수를 종료해버리면 아래쪽의 이동 로직이 실행 안 됨
        }
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
        
        yaw += mouseDeadzone.x * cameraSpeed * Time.deltaTime;
       

        yaw=Mathf.Clamp(yaw, yawLimit.x, yawLimit.y);
        

        playerCamera.rotation = Quaternion.Euler(pitch, yaw, 0);
        RotateFlashlightToMouse();
        if (isFlashlightOn)
        {
            
            CheckForMonsters();
        }
    }
    void CheckForMonsters()
    {
        // 손전등의 현재 방향으로 레이캐스트를 쏩니다.
        // RotateFlashlightToMouse()에 의해 회전된 flashlight.transform.forward를 사용합니다.
        Ray ray = new Ray(flashlight.transform.position, flashlight.transform.forward);
        RaycastHit hit;

        // 사거리 15m (원하시는 대로 조절 가능)
        if (Physics.Raycast(ray, out hit, 15f))
        {
            
            // 맞은 물체의 태그가 Monster라면
            if (hit.collider.CompareTag("Monster"))
            {
                Debug.Log("눈알 맞음");
                // EyeMonster 컴포넌트를 가져와서 대미지 함수 실행
                EyeMonster monster = hit.collider.GetComponent<EyeMonster>();
                if (monster != null)
                {
                    Debug.Log("눈알 데미지 맞음");
                    monster.TakeLightDamage();
                }
            }
        }

        // 개발 중 확인용 노란 선 (Scene 뷰에서만 보임)
        Debug.DrawRay(flashlight.transform.position, flashlight.transform.forward * 15f, Color.yellow);
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
