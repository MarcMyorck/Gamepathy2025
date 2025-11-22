using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameFinishManager : MonoBehaviour
{
    public Button start;
    public Button exit;
    public TextMeshProUGUI scoreText1;
    public TextMeshProUGUI scoreText2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        start.onClick.AddListener(StartGame);
        exit.onClick.AddListener(ExitGame);
        scoreText1.text = "Carts collected: " + SessionData.cartsCollected + "/" + SessionData.cartsTotal;
        scoreText2.text = "Empathie collected: " + SessionData.empathieCollected + "/" + SessionData.empathieTotal;
        SessionData.Reset();
    }

    void StartGame()
    {
        SceneManager.LoadScene("Tutorial");
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
