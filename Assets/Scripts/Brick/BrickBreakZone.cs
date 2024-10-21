using UnityEngine;

public class BrickBreakZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Brick"))
        {
            Debug.Log("Force destroy");
            Brick brick = collider.GetComponent<Brick>();
            brick.Hit("", 0, true);
        }
    }
}