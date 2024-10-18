using UnityEngine;

public enum ItemEffectType
{
    AddLife,
    BreakAnyBrick,
    AddVelocityToBall,
    AddVelocityToPaddle,
}

public abstract class Item : MonoBehaviour
{
    // 아이템에 따라 다른 효과. 행위가 각각 다름. 
    // 추상 메서드 사용 -> 추상 메서드 사용을 위한 추상 클래스 사용
    // 자식 클래스에서 구체적인 사항 명시
    public abstract void Use(GameObject paddle);

    // 떨어지는 기능 : 닿는 대상은 패들이랑만 닿아야 한다 : 트리거
    // 패들에 닿으면 발동 : 트리거에서 패들이랑 닿으면 발동하도록
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Paddle"))
        {
            Use(collision.gameObject); // 자식에서 구현된 메서드 호출
            Destroy(gameObject); // 사용 후 파괴
        }
    }
}