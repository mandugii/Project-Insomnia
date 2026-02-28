using System.Collections;
using UnityEngine;

public class ClosetMonster : MonoBehaviour
{
    private Animator anim;
    
    public AudioSource asource;
    public AudioClip apear, disapear, attack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }
    void Update()
    {

        // 플레이어가 창문 앞에 있고 + 손전등으로 비추고 있다면
        if (CheckIfPlayerIsWatching()&& Flash.isFlashlightOn)
        {
            anim.SetBool("apear", false);
            asource.PlayOneShot(disapear);
            StopAllCoroutines();
        }
        
    }
    public void StartAnim()
    {
        asource.PlayOneShot(apear);
        anim.SetBool("apear", true);
        StartCoroutine(AttackPlayer());
    }
    
    IEnumerator AttackPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            anim.SetTrigger("Attack");
            asource.PlayOneShot(attack);
        }
    }
    
    

    bool CheckIfPlayerIsWatching()
    {
        if (PlayerState.ps.pState == PlayerState.State.Closet)
        {
            return true;
        }
        else return false;
    }

}
