using UnityEngine;

public class GameUI : MonoBehaviour
{
    public GameObject windowReturnBtn;
    public GameObject closetReturnBtn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        windowReturnBtn.SetActive(false);
        closetReturnBtn.SetActive(false);
    }

    
    public void activeWindowReturnBtn()
    {
        windowReturnBtn.SetActive(true);
    }
    public void activeClosetReturnBtn()
    {
        closetReturnBtn.SetActive(true);
    }
    public void deactiveWindowReturnBtn()
    {
        windowReturnBtn.SetActive(false);
    }
    public void deactiveClosetReturnBtn()
    {
        closetReturnBtn.SetActive(false);
    }

}
