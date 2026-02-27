using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState ps;
    public enum State
    {
        Idle,
        Flash,
        Move,
        Sleep,
        Attacked,
        GameOver
    }
    public State pState;

    private void Awake()
    {
        if (ps == null) ps = this;
    }
    public State checkState()
    {
        return pState;
    }
    public void pIdle()
    {
        pState = State.Idle;
        Debug.Log("통상 상태");
    }
    public void pFlash()
    {
        pState = State.Flash;
        Debug.Log("손전등 작동");
    }
    public void pMove()
    {
        pState = State.Move;
        Debug.Log("플레이어 이동");
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
