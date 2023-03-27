using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject buttonsCointainer;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject _continueButton;
    public static bool saveExist = false;

    private void Awake()
    {
        saveExist = SaveSystem.LoadData(nameof(saveExist), saveExist);
        if(saveExist) _continueButton.SetActive(true);
    }

    public void StartButton()
    {
        saveExist = false;
        SaveSystem.SaveData(nameof(saveExist), saveExist);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SettingsButton()
    {
       // settingsPanel.SetActive(true);
       // buttonsCointainer.SetActive(false);
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
