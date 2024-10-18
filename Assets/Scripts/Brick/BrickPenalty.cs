using System.Collections;
using UnityEngine;

public class BrickPenalty : MonoBehaviour
{
    [SerializeField] [Range(1.1f, 2f)] float sizeUpPercent = 1.5f;
    [SerializeField] AnimationCurve curve;
    
    Coroutine animateHandler;
    WaitForSeconds wait = new WaitForSeconds(0.02f);

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            SizeUp();
        }
    }

    void SizeUp()
    {
        if (animateHandler != null)
            return;

        animateHandler = StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        float progress = 0f;
        Vector2 startSize = transform.localScale;
        Vector2 endSize = transform.localScale * 1.5f;

        while (progress <= 1f)
        {
            transform.localScale = Vector2.Lerp(startSize, endSize, curve.Evaluate(progress));

            yield return wait;

            progress += 0.1f;
        }

        animateHandler = null;
    }


}
