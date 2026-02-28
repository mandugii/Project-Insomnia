using UnityEngine;

public class WindowMonster : MonoBehaviour
{
    private Animator anim;
    private bool isMonsterWatching=false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim=GetComponent<Animator>();
        
    }
    void Update()
    {

        // 플레이어가 창문 앞에 있고 + 손전등으로 비추고 있다면
        if (CheckIfPlayerIsWatching())
        {
            StopClimbing();
            if (Flash.isFlashlightOn&&isMonsterWatching)
            {
                anim.speed = 1f;
                anim.SetBool("ClimbStart", false);
            }
        }
        else
        {
            ResumeClimbing();
        }
    }
    public void StartAnim()
    {
        anim.SetBool("ClimbStart", true);
    }
    void StopClimbing()
    {
        // 애니메이션 재생 속도를 0으로 만들어 그 자리에서 멈춤
        anim.speed = 0f;
        // 여기서 괴물이 부르르 떨거나 플레이어를 째려보는 연출을 넣으면 더 무섭습니다.
    }
    public void StartWatching() // 문이 열리는 애니메이션의 '틈이 생기는' 프레임에 배치
    {
        isMonsterWatching = true;
        Debug.Log("⚠️ 괴물 감시 시작");
    }

    public void StopWatching() // 문이 닫히는 애니메이션의 '틈이 없어지는' 프레임에 배치
    {
        isMonsterWatching = false;
        Debug.Log("✅ 괴물 감시 종료");
    }
    void ResumeClimbing()
    {
        anim.speed = 1f; // 다시 등반 시작
    }

    bool CheckIfPlayerIsWatching()
    {
        if (PlayerState.ps.pState == PlayerState.State.Window)
        {
            return true;
        }
        else return false;
    }
    
   
}
