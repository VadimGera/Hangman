using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HangmanScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textField;
    [SerializeField] private TextMeshProUGUI hintsTextField;
    [SerializeField] private int hp = 6;
    [SerializeField] private TextMeshProUGUI hpCurrent;
    [SerializeField] private GameObject restartPanelLose;
    [SerializeField] private GameObject restartPanelWin;

    private readonly List<char> _guessedLetters = new();
    private readonly List<char> _wrongTriedLetter = new();

    private readonly string[] _words =
    {
        "Tree",
        "Dog",
        "Rain",
        "Program",
        "PC",
        "Snow",
        "Phone"
    };

    private readonly string[] _hints =
    {
        "green leaves in summer",
        "walking on the street",
        "in bad autumn weather",
        "need to make it work",
        "we are working on it",
        "outdoors in winter",
        "you can call it"
    };

    private string _wordToGuess = "";
    private string _hint = "";

    private KeyCode _lastKeyPressed;

    private void Start()
    {
        var randomIndex = Random.Range(0, _words.Length);

        _wordToGuess = _words[randomIndex];
        _hint = _hints[randomIndex];
        hintsTextField.text = _hint;
    }


    private void OnGUI()
    {
        var e = Event.current;
        if (!e.isKey) return;
        if (e.keyCode == KeyCode.None || _lastKeyPressed == e.keyCode) return;
        ProcessKey(e.keyCode);

        _lastKeyPressed = e.keyCode;
    }

    private void ProcessKey(KeyCode key)
    {
        print("Key Pressed: " + key);

        var pressedKeyString = key.ToString()[0];

        var wordUppercase = _wordToGuess.ToUpper();

        var wordContainsPressedKey = wordUppercase.Contains(pressedKeyString);
        var letterWasGuessed = _guessedLetters.Contains(pressedKeyString);

        if (!wordContainsPressedKey && !_wrongTriedLetter.Contains(pressedKeyString))
        {
            _wrongTriedLetter.Add(pressedKeyString);
            hp -= 1;


            if (hp <= 0)
            {
                restartPanelLose.SetActive(true);
            }
            else
            {
                hpCurrent.text = hp.ToString();
            }
        }

        if (wordContainsPressedKey && !letterWasGuessed)
        {
            _guessedLetters.Add(pressedKeyString);
        }

        var stringToPrint = "";
        foreach (var letterInWord in wordUppercase)
        {
            if (_guessedLetters.Contains(letterInWord))
            {
                stringToPrint += letterInWord;
            }
            else
            {
                stringToPrint += "_";
            }
        }

        if (wordUppercase == stringToPrint)
        {
            restartPanelWin.SetActive(true);
        }
        
        print(stringToPrint);
        textField.text = stringToPrint;
    }
}
