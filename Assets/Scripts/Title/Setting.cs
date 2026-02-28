using System.Collections;
using UnityEngine;

public class Setting : MonoBehaviour
{
    public Animator anim;
    public Camera cam;
    public GameObject selectedUI;
   
    public float rotTime = 0.5f;
    // Update is called once per frame
    public void ClickOnSetting()
    {
        selectedUI.SetActive(false);
        anim.SetBool("SettingOn",true);
        
    }
    public void ClickOnBack()
    {
        selectedUI.SetActive(true);
        anim.SetBool("SettingOn", false);

    }
    
}
