using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    InputHandler ih;
    public GameObject pauseMenu;
    public Button continueBtn;
    public Button mainMenuBtn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenu.SetActive(false);
        continueBtn.onClick.AddListener(ContinueGame);
        mainMenuBtn.onClick.AddListener(ExitToMainMenu);
    }

    public void Pause(InputHandler ihtemp)
    {
        ih = ihtemp;
        Time.timeScale = 0f;               // stops physics, animations and most Update-based logic
        Time.fixedDeltaTime = 0.02f * Time.timeScale; // keep fixedDelta consistent if you change it elsewhere
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenu.SetActive(true);
    }

    public void UnPause()
    {
        ih.isPaused = false;
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
    }

    void ContinueGame()
    {
        UnPause();
    }

    void ExitToMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
