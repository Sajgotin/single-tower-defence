using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonsCointainer;
    [SerializeField]
    private GameObject settingsPanel;

    public void StartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SettingsButton()
    {
        settingsPanel.SetActive(true);
        buttonsCointainer.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    private void Start()
    {
        settingsPanel.SetActive(false);
        buttonsCointainer.SetActive(true);
    }
}
