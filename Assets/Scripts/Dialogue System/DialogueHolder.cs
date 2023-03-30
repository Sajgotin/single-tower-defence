using System.Collections;
using UnityEngine;

public class DialogueHolder : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(DialogueSequence());
    }

    private IEnumerator DialogueSequence()
    {
        Time.timeScale = 0.0f;
        for(int i = 0; i < transform.childCount; i++)
        {
            Deactivate();
            transform.GetChild(i).gameObject.SetActive(true);
            yield return new WaitUntil(() => transform.GetChild(i).GetComponent<DialogueLine>().finished);
        }
        gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    private void Deactivate()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
