using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed;

    private float movement;
    private float xMaxPosition = 2.25f;
    private float xMinPosition = -2.25f;
    private Vector3 startPosition;

    public string playerName;


    void Start()
    {
        startPosition = transform.position;
        playerName  = ScoreManager.Instance.player1Name;
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            movement += 1f;
            MovePaddle();
        }

        if (Input.GetKey(KeyCode.A))
        {
            movement -= 1f;
            MovePaddle();
        }
    }

    void MovePaddle()
    {
        transform.position += (new Vector3(movement * speed, 0)).normalized * Time.deltaTime * speed;

        if(transform.position.x > xMaxPosition)
        {
            transform.position = new Vector3(xMaxPosition, transform.position.y);
        }
       
        if(transform.position.x < xMinPosition)
        {
            transform.position = new Vector3(xMinPosition, transform.position.y);
        }

        movement = 0f;
    }

    public void Reset()
    {
        transform.position = startPosition;
    }
}
