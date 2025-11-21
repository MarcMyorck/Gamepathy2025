using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenuManager : MonoBehaviour
{
    public Button exit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        exit.onClick.AddListener(ExitSettings);
    }

    void ExitSettings()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
