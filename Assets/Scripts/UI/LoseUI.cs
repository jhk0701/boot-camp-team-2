using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoseUI : MonoBehaviour
{
    [SerializeField] Button retryButton;
    [SerializeField] Button homeButton;

    // Start is called before the first frame update
    void Start()
    {
        retryButton.onClick.AddListener(Retry);
        homeButton.onClick.AddListener(LoadHome);
    }

    // Update is called once per frame
    public void Retry()
    {
        gameObject.SetActive(false);
        // SceneManager.LoadScene("GameScene");
    }

    public void LoadHome()
    {
        //Startæ¿¿∏∑Œ
        GameManager.Instance.BackToLobby();
    }
}
