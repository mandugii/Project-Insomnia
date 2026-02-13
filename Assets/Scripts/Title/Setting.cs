using System.Collections;
using UnityEngine;

public class Setting : MonoBehaviour
{
    public Camera cam;
    public GameObject selectedUI;
    private bool check=false;
    public float rotTime = 0.5f;
    // Update is called once per frame
    public void ClickOnSetting()
    {
        selectedUI.SetActive(false);
        check = false;
        StartCoroutine(RotateCam(Vector3.up * -90,check));
    }
    public void ClickOnBack()
    {
        check = true;
        StartCoroutine(RotateCam(Vector3.up * 90,check));
        
    }
    IEnumerator RotateCam(Vector3 angle,bool check)
    {
        Quaternion startRot = cam.transform.rotation;
        Quaternion endRot = startRot * Quaternion.Euler(angle);
        
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / rotTime;
            cam.transform.rotation = Quaternion.Slerp(startRot, endRot, t);
            yield return null;
        }
        
        cam.transform.rotation = endRot;
        if (check)
        {
            selectedUI.SetActive(true);
        }
    }
}
