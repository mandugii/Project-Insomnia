using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Play : MonoBehaviour
{
    public GameObject panel;
    private CanvasGroup panelGroup;
    public float fadeTime = 2f;
    
    public void ClickOnPlay()
    {
        panel.SetActive(true);
        StartCoroutine(fadeOut());
    }
    IEnumerator fadeOut()
    {

        
        panelGroup = panel.GetComponent<CanvasGroup>();
        float currentTime = 0f;
        while(currentTime < 1f)
        {
            currentTime += Time.deltaTime / fadeTime;
            panelGroup.alpha=Mathf.Lerp(0,1,currentTime);
            yield return null;
        }
        panelGroup.alpha = 1;
        SceneManager.LoadScene("Insomnia");
    }
}
