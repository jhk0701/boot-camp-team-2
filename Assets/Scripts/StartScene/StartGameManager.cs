using UnityEngine;
using UnityEngine.SceneManagement; 

public class StartGameManager : MonoBehaviour
{
    // Start ��ư�� Ŭ���� �� ȣ��� �޼���
    public void OnStartButtonClicked()
    {
        Debug.Log("Start Button Clicked. Loading MainScene...");

        // MainScene���� ��ȯ (���� ���� �� �̸��� "MainScene"�̶�� ����)
        SceneManager.LoadScene("SampleScene");

        // GameManager �� �ʿ��� �ʱ�ȭ �۾��� �ִٸ� ���⿡ �߰�
    }
}