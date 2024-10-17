using UnityEngine;
using UnityEngine.SceneManagement; 

public class StartGameManager : MonoBehaviour
{
    // Start 버튼이 클릭될 때 호출될 메서드
    public void OnStartButtonClicked()
    {
        Debug.Log("Start Button Clicked. Loading MainScene...");

        // MainScene으로 전환 (메인 게임 씬 이름이 "MainScene"이라고 가정)
        SceneManager.LoadScene("SampleScene");

        // GameManager 등 필요한 초기화 작업이 있다면 여기에 추가
    }
}