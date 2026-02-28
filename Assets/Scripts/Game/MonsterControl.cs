using DoorScript;
using System.Collections;
using UnityEngine;

public class MonsterControl : MonoBehaviour
{
    public WindowMonster win;
    public ClosetMonster closet;
    public DoorMonster door;
    public Clock clock;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        StartCoroutine(MonsterControlSystem());
    }

    IEnumerator MonsterControlSystem()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            float waitTime = Random.Range(10f, 20f);
            yield return new WaitForSeconds(waitTime);
            if (clock.hour>=0&& clock.hour < 6)
            {
                
                
                // 0~2 사이의 숫자를 무작위로 선택 (3은 미포함)
                int eventIndex = 2;//Random.Range(0, 3);

                switch (eventIndex)
                {
                    case 0:
                        Debug.Log("노크 당첨");
                        door.StartAnim(); // 문 노크 이벤트 실행
                        yield return new WaitForSeconds(27f);
                        break;
                    case 1:
                        Debug.Log("창문 당첨");
                        win.StartAnim(); // 창문 등반 시작
                        yield return new WaitForSeconds(17f);
                        break;
                    case 2:
                        Debug.Log("옷장 당첨");
                        closet.StartAnim(); // 옷장 괴물 감시 시작
                        yield return new WaitForSeconds(10f);
                        break;
                }
            }
            
            else if (clock.hour >= 6)
            {

            }
        }
    }
}
