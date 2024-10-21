using UnityEngine;

public abstract class Item : MonoBehaviour
{    
    protected GameObject collidedObject;
    public ItemEffect itemEffect;

    // 아이템에 따라 다른 효과. 행위가 각각 다름. 
    // 구체적인 사항을 자식 클래스에서 구현
    protected abstract void Use();
    public abstract void EndEffect(ItemEffect effect);

    // 떨어지는 기능 : 닿는 대상은 패들이랑만 닿아야 한다 : 트리거
    // 패들에 닿으면 발동 : 트리거에서 패들이랑 닿으면 발동하도록
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().Sleep();

            GameManager.Instance.soundManager.PlaySfx(SfxType.ItemUsage);

            collidedObject = collision.gameObject;
            Use(); // 자식에서 구현된 메서드 호출
        }
        else if(collision.gameObject.CompareTag("DownWall"))
        {
            DestoryItem();
        }
    }

    public virtual bool Initialize()
    {
        bool isAdded = GameManager.Instance.ItemHandler.ActivateEffect(itemEffect);

        if (isAdded)
            GameManager.Instance.ItemHandler.OnEffectEnded += EndEffect;

        return isAdded;
    }

    public virtual void DestoryItem()
    {
        GameManager.Instance.ItemHandler.OnEffectEnded -= EndEffect;
        Destroy(gameObject); 
    }
    
}