using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float maxTime = 60f; 
    private float remainTime;
    private float elapsedTime;
    private bool isPlaying;


    public void StartTimer()
    {
        elapsedTime = 0f;
        remainTime = maxTime;
        isPlaying = true;
    }

    public void StopTimer()
    {
        isPlaying = false;
    }

    public void PauseTimer()
    {
        isPlaying = false;
    }

    public void ResumeTimer()
    {
        isPlaying = true;
    }

    private void Update()
    {
        if (isPlaying)
        {
            remainTime -= Time.deltaTime;
            
            elapsedTime += Time.deltaTime;
        }
    }

    public float GetRemainingTime()
    {
        return remainTime;
    }

    public float GetElaspedTime()
    {
        return elapsedTime;
    }

    public bool CheckRemainTime()
    {
        return remainTime <= 0 ? true : false;
    }
}
