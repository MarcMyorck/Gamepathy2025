using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Button start;
    public Button exit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        start.onClick.AddListener(StartGame);
        exit.onClick.AddListener(ExitGame);
    }

    void StartGame()
    {
        SceneManager.LoadScene("Level");
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
