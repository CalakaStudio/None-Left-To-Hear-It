using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class TypewriterEffect : MonoBehaviour
{
    //DuctTape
    [SerializeField] string TextToWrite;
    [SerializeField] private TMP_Text _textBox;

    //Basic Typewriter Functionality
    private int _currentVissibleCharacterIndex;
    private Coroutine _typewriterCoroutine;

    private WaitForSeconds _simpleDelay;
    private WaitForSeconds _interpunctuationDelay;

    [Header("TypewriterSettings")]
    [SerializeField] float charactersPerSecond = 20;
    [SerializeField] float interpunctuationDelay = 0.5f;


    //Skipping Functionality
    public bool CurrentlySkipping { get; private set; }
    private WaitForSeconds _skipDelay;

    [Header("Skip options")]
    [SerializeField] private bool quickSkip;
    [SerializeField] [Min(1)] private int skipSpeedup = 5;

    //ChangeScene References
    [SerializeField] ChangeDemoScreen sceneChange;

    void Awake()
    {
        _textBox = GetComponent<TMP_Text>();

        _simpleDelay = new WaitForSeconds(1 / charactersPerSecond);
        _interpunctuationDelay = new WaitForSeconds(interpunctuationDelay);

        _skipDelay = new WaitForSeconds(1/ (charactersPerSecond * skipSpeedup));
    }
    
    void Start()
    {
        SetText(TextToWrite);
    }

    
    public void ButtonCall()
    {
        if(_textBox.maxVisibleCharacters != _textBox.textInfo.characterCount)
        {
            Debug.Log("SkippingText");
            Skip();
        }
        else
        {
            Debug.Log("SkippingText");
            sceneChange.ChangeScene();
        }
        
    }


    

    public void SetText(string text)
    {
        if(_typewriterCoroutine != null)
        {
            StopCoroutine(_typewriterCoroutine);
        }
        _textBox.text = text;  
        _textBox.maxVisibleCharacters = 0;
        _currentVissibleCharacterIndex = 0;

        _typewriterCoroutine = StartCoroutine(routine: Typewriter());
    }

    private IEnumerator Typewriter()
    {
        TMP_TextInfo textInfo = _textBox.textInfo;
        while (_currentVissibleCharacterIndex < textInfo.characterCount + 1)
        {

            char character = textInfo.characterInfo[_currentVissibleCharacterIndex].character;
            _textBox.maxVisibleCharacters++;


            if(!CurrentlySkipping && (character == '?' || character == ',' ||character == '.' ||character == ';' ||character == ':' ||character == '!' ||character == '-'))
            {
                yield return _interpunctuationDelay;
            }
            else
            {
                yield return CurrentlySkipping ? _skipDelay : _simpleDelay;
            }

            _currentVissibleCharacterIndex++;
        }
    }

    void Skip()
    {
        if(CurrentlySkipping)
            return;

        CurrentlySkipping = true;

        if(!quickSkip)
        {
            StartCoroutine(routine: SkipSpeedupReset());
            return;
        }

        StopCoroutine(_typewriterCoroutine);
        _textBox.maxVisibleCharacters = _textBox.textInfo.characterCount;
    }

    private IEnumerator SkipSpeedupReset()
    {
        yield return new WaitUntil(() => _textBox.maxVisibleCharacters == _textBox.textInfo.characterCount - 1);
        CurrentlySkipping = false;
    }
}
