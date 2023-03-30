using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueLine : DialogueBaseClass
{
    [Header("Text Options")]
    [SerializeField] TMP_Text _textHolder;
    [SerializeField] string _input;
    [SerializeField] Color _textColor;
    [SerializeField] TMP_FontAsset _textFont;

    [Header("Time parameters")]
    [SerializeField] float _delay;

    [Header("Character Image")]
    [SerializeField] Sprite _avatarSprite;
    [SerializeField] Image _imageHolder;

    private IEnumerator _lineAppear;

    private void Awake()
    {
        _textHolder.text = "";

        _imageHolder.sprite = _avatarSprite;
        _imageHolder.preserveAspect = true;
    }

    private void OnEnable()
    {
        _lineAppear = WriteText(_input, _textHolder, _textColor, _textFont, _delay);
        StartCoroutine(_lineAppear);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_textHolder.text != _input) 
            {
                StopCoroutine(_lineAppear);
                _textHolder.text = _input; 
            }
            else finished = true;
        }
    }
}
