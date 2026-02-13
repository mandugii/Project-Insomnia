using UnityEngine;
using UnityEngine.InputSystem;

public class Flash : MonoBehaviour
{
    [Header("연결 설정")]
    public Transform playerCamera;
    public Light flashlight;
    public CanvasGroup eyeClosedOverlay; // 눈 감기 연출용 블랙 이미지

    

    private Vector2 mouseInput;
    private bool isFlashlightOn;
    public void Start()
    {
        flashlight.enabled = false;
    }
    
    

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
