using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBaseClass : MonoBehaviour
{
    public bool finished {  get; protected set; }

    protected IEnumerator WriteText(string input, TMP_Text textHolder, Color textColor, TMP_FontAsset textFont, float delay)
    {
        textHolder.color = textColor;
        textHolder.font = textFont;

        for(int i = 0; i < input.Length; i++)
        {
            textHolder.text += input[i];
            SoundController.Instance.PlayDialogueSound();
            yield return new WaitForSecondsRealtime(delay);
        }

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        finished = true;
    }
}
