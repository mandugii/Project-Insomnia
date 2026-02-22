using UnityEngine;

public class EyeMonster : MonoBehaviour
{
    public float health = 100f;
    public float meltSpeed = 50f; // 초당 깎일 체력
    private Material targetMaterial;

    void Start()
    {
        // 머티리얼의 투명도를 조절하기 위해 가져옵니다. 
        // (주의: Rendering Mode가 Transparent여야 함)
        targetMaterial = GetComponent<Renderer>().material;
    }

    public void TakeLightDamage()
    {
        health -= meltSpeed * Time.deltaTime;

        // 시각적 효과: 체력이 낮아질수록 투명해짐
        if (targetMaterial != null)
        {
            Color color = targetMaterial.color;
            color.a = health / 100f;
            targetMaterial.color = color;
        }

        if (health <= 0)
        {
            // 사라질 때 효과음이나 파티클을 넣으면 좋습니다.
            Destroy(gameObject);
        }
    }
}
