using System.Collections;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance { get; private set; }
    [SerializeField] GameObject _cutsceneCanvas;
    [SerializeField] GameObject _statsPanel;
    [SerializeField] GameObject _healthBarCanvas;
    [SerializeField] GameObject _crosshair;
    [SerializeField] GameObject[] _dialogueHolders;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void CutsceneCheck(int wave)
    {
        if (wave == 1)
        {
            StartCoroutine(PlayCutscene(0));
        }
    }

    IEnumerator PlayCutscene(int dialogueHolderIndex)
    {
        _healthBarCanvas.GetComponent<CanvasGroup>().alpha = 0;
        _dialogueHolders[dialogueHolderIndex].SetActive(true);
        _statsPanel.SetActive(false);
        _crosshair.SetActive(false);
        _cutsceneCanvas.SetActive(true);

        yield return new WaitUntil(() => Time.timeScale > 0);
        _statsPanel.SetActive(true);
        _crosshair.SetActive(true);
        _healthBarCanvas.GetComponent<CanvasGroup>().alpha = 1;
        _cutsceneCanvas.SetActive(false);
    }
}
