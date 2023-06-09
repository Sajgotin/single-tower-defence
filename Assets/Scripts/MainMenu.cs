using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject buttonsCointainer;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject _continueButton;
    [SerializeField] GameObject _settingsButton;
    public static bool saveExist = false;

    private void Awake()
    {
        saveExist = SaveSystem.LoadData(nameof(saveExist), saveExist);
        if(saveExist) _continueButton.SetActive(true);
    }

    public void ContinueButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartButton()
    {
        saveExist = false;
        SaveSystem.SaveData(nameof(saveExist), saveExist);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SettingsButton()
    {
        settingsPanel.SetActive(true);
        buttonsCointainer.SetActive(false);
    }

    public void BackButton()
    {
        settingsPanel.SetActive(false);
        buttonsCointainer.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_settingsButton);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    private void Start()
    {
        buttonsCointainer.SetActive(true);
    }
}
