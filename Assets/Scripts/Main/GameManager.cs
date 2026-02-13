using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (gm != null)
        {
            Destroy(gameObject);
            return;
        }
        gm = this;
        DontDestroyOnLoad(gameObject);
    }
    public enum GameState
    {
        Stop,
        Run,
        CutSceen,
        Title,
        GameOver
    }
    //현재 게임 상태 변수
    public GameState gState;
    void Start()
    {
      
    }

    // Update is called once per frame
    
    public void stop()
    {
        Debug.Log("정지");
        gState = GameState.Stop;
        Time.timeScale = 0f;

        if (Cursor.lockState == CursorLockMode.Locked && Cursor.visible == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        return;
    }
    public void run()
    {
        Debug.Log("실행");
        gState =GameState.Run;
        Time.timeScale = 1f;
        if (Cursor.lockState == CursorLockMode.None && Cursor.visible == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    public void cutSceen()
    {
        Debug.Log("컷씬");
        gState = GameState.CutSceen;
        Time.timeScale = 1f;
        if (Cursor.lockState == CursorLockMode.None && Cursor.visible == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        return ;
    }
}
