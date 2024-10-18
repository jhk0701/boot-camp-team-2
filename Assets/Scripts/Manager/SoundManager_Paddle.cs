using UnityEngine;

public class PaddleHitSound : MonoBehaviour
{
    public AudioClip hitSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = hitSound;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Paddle")
        {
            audioSource.Play();
        }
    }
}
