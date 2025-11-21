using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameFinishManager : MonoBehaviour
{
    public Button start;
    public Button exit;
    public TextMeshProUGUI scoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        start.onClick.AddListener(StartGame);
        exit.onClick.AddListener(ExitGame);
        scoreText.text = "Carts collected: " + SessionData.cartsCollected + "/" + SessionData.cartsTotal;
        SessionData.Reset();
    }

    void StartGame()
    {
        SceneManager.LoadScene("Level");
        SessionData.Reset();
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
