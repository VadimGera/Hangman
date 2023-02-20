using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HangmanScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textField;
    [SerializeField] private int hp = 6;
    [SerializeField] private TextMeshProUGUI _hpCurrent;
    [SerializeField] private GameObject restartPanelLose;
    [SerializeField] private GameObject restartPanelWin;
        
        private List<char> guessedLetters = new List<char>();
        private List<char> wrongTriedLetter = new List<char>();

        private string[] words =
        {
            "Tree",
            "Dog",
            "Rain",
            "Program"
        };

        private string wordToGuess = "";
        
        private KeyCode lastKeyPressed;

        private void Start()
        {
            var randomIndex = Random.Range(0, words.Length);

            wordToGuess = words[randomIndex];
        }


        void OnGUI()
        {
            Event e = Event.current;
            if (e.isKey)
            {
                // Debug.Log("Detected key code: " + e.keyCode);

                if (e.keyCode != KeyCode.None && lastKeyPressed != e.keyCode)
                {
                    ProcessKey(e.keyCode);
                    
                    lastKeyPressed = e.keyCode;
                }
            }
        }

        private void ProcessKey(KeyCode key)
        {
            print("Key Pressed: " + key);

            var pressedKeyString = key.ToString()[0];

            var wordUppercase = wordToGuess.ToUpper();
            
            var wordContainsPressedKey = wordUppercase.Contains(pressedKeyString);
            bool letterWasGuessed = guessedLetters.Contains(pressedKeyString);

            if (!wordContainsPressedKey && !wrongTriedLetter.Contains(pressedKeyString))
            {
                wrongTriedLetter.Add(pressedKeyString);
                hp -= 1;
                

                if (hp <= 0)
                {
                    restartPanelLose.SetActive(true);
                }
                else
                {
                    _hpCurrent.text = hp.ToString();
                }
            }
            
            if (wordContainsPressedKey && !letterWasGuessed)
            {
                guessedLetters.Add(pressedKeyString);
            }

            string stringToPrint = "";
            for (int i = 0; i < wordUppercase.Length; i++)
            {
                char letterInWord = wordUppercase[i];

                if (guessedLetters.Contains(letterInWord))
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
            
            // print(string.Join(", ", guessedLetters));
            print(stringToPrint);
            _textField.text = stringToPrint;
        }
}
