using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
namespace DoorScript
{
	[RequireComponent(typeof(AudioSource))]


	public class DoorMonster : MonoBehaviour
	{
		
		public bool knock=false;
		
		public Animator anim;
		public AudioSource asource;
		public AudioClip openDoor, closeDoor, knockDoor;
		private bool isMonsterWatching;
        // Use this for initialization
        void Start()
		{
			asource = GetComponent<AudioSource>();
			
            

        }

        // Update is called once per frame
        void Update()
        {
            // [핵심] 괴물이 감시 중일 때 플레이어 상태 체크
            if (isMonsterWatching)
            {
                
                if (PlayerState.ps.pState != PlayerState.State.Idle||Flash.isFlashlightOn)
                {
                    isMonsterWatching = false; // 중복 실행 방지
                    StopAllCoroutines();
                    jumpScare();
                }
            }
        }
        public void StartAnim()
        {
            StartCoroutine(randomKnock());
        }
        IEnumerator randomKnock()
		{
            
            while (true) {
               
				KnockDoor();
                Debug.Log("노크 실행됨");
                yield return new WaitForSeconds(7f);
				OpenDoor();
                yield return new WaitForSeconds(10f);
				CloseDoor();
				yield return new WaitForSeconds(10f);
                break;
            }
		}
        public void StartWatching() // 문이 열리는 애니메이션의 '틈이 생기는' 프레임에 배치
        {
            isMonsterWatching = true;
            Debug.Log("⚠️ 괴물 감시 시작");
        }

        public void StopWatching() // 문이 닫히는 애니메이션의 '틈이 없어지는' 프레임에 배치
        {
            isMonsterWatching = false;
            Debug.Log("✅ 괴물 감시 종료");
        }
        public void KnockDoor()
        {
			knock = true;
			asource.PlayOneShot(knockDoor);
			
        }
        public void OpenDoor()
		{
			
			asource.PlayOneShot(openDoor);
			anim.SetBool("Open", true);
        }
        public void CloseDoor()
        {
           
            asource.PlayOneShot(closeDoor);
            anim.SetBool("Open", false);
        }
        public void jumpScare()
        {
            PlayerState.ps.pAttacked();

            anim.SetTrigger("Attack");
        }
    }
}