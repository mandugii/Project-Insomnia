using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public GameUI ui;
    public static PlayerState ps;
    public enum State
    {
        Idle,
        
        Move,
        Sleep,
        Attacked,
        Window,
        Closet,
        GameOver
    }
    public State pState;

    private void Awake()
    {
        if (ps == null) ps = this;
    }
    
    public void pIdle()
    {

        pState = State.Idle;
        Debug.Log("통상 상태");
    }
    
    public void pMove()
    {
        pState = State.Move;
        Debug.Log("플레이어 이동");
    }
    public void pWindow()
    {
        ui.activeWindowReturnBtn();
        pState = State.Window;
        Debug.Log("창문 관찰 상태");
    }
    public void pCloset()
    {
        ui.activeClosetReturnBtn();
        pState = State.Closet;

        Debug.Log("옷장 관찰 상태");
    }
    public void pSleep()
    {
        pState = State.Sleep;
        Debug.Log("잠자기 상태");

    }
    public void pAttacked()
    {
        pState = State.Attacked;
        Debug.Log("갑툭튀");
    }
    public void pGameOver()
    {
        pState=State.GameOver;
        Debug.Log("게임 오버");
    }
}
