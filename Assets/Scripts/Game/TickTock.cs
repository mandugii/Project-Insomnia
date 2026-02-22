using UnityEngine;

public class TickTock : MonoBehaviour
{
    public AudioClip clockSound;
    public AudioSource source;
    private float oneSecond = 1f;
    private float currentTime = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
 

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > oneSecond)
        {
            source.pitch = Random.Range(1.75f, 1.85f);
            source.PlayOneShot(clockSound);
            currentTime = 0f;
        }
    }
}
