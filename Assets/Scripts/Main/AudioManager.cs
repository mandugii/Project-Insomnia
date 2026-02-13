using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager am; // singleTone

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    const string MASTER = "MasterVol";
    const string BGM = "BGMVol";
    const string SFX = "SFXVol";

    private void Awake()
    {
        if(am != null) { 
            Destroy(gameObject);
            return;
        }
        am = this;
        DontDestroyOnLoad(gameObject);
       
    }

    public void SetMaster(float v) { mixer.SetFloat(MASTER, ToDb(v)); }
    public void SetBGM(float v) { mixer.SetFloat(BGM, ToDb(v)); }
    public void SetSFX(float v) { mixer.SetFloat(SFX, ToDb(v)); }

    static float ToDb(float v)
    {
        v = Mathf.Clamp(v, 0.0001f, 1f);
        return Mathf.Log10(v) * 20f;
    }
    // BGM 재생
    public void PlayBGM(AudioClip clip)
    {
        if (bgmSource.clip != clip && bgmSource.isPlaying) return;
        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    // 효과음 재생
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

}