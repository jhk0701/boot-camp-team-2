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
    // �����ۿ� ���� �ٸ� ȿ��. ������ ���� �ٸ�. 
    // �߻� �޼��� ��� -> �߻� �޼��� ����� ���� �߻� Ŭ���� ���
    // �ڽ� Ŭ�������� ��ü���� ���� ���
    public abstract void Use(GameObject paddle);

    // �������� ��� : ��� ����� �е��̶��� ��ƾ� �Ѵ� : Ʈ����
    // �е鿡 ������ �ߵ� : Ʈ���ſ��� �е��̶� ������ �ߵ��ϵ���
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Paddle"))
        {
            Use(collision.gameObject); // �ڽĿ��� ������ �޼��� ȣ��
            Destroy(gameObject); // ��� �� �ı�
        }
    }
}