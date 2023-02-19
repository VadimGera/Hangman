using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class HangmanScript : MonoBehaviour
{
    
        [SerializeField] private TextMeshProUGUI _hpText;
        [SerializeField] private TextMeshProUGUI _letterText;
        [SerializeField] private TextMeshProUGUI _fieldText;
        [SerializeField] private int hp = 7;


        public Restart gameManagerLose;
        public Restart gameManagerWin;

        private List<char> guessedLetters = new List<char>();
        private List<char> wrongTriedLetter = new List<char>();

        

        private string initialWord = "";

        private char[] Letters =
        {
            'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P',
            'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L',
            'Z', 'X', 'C', 'V', 'B', 'N', 'M'
        };

        private string[] words =
        {
            "Mouse",
            "Tree",
            "Unity",
            "Program"
        };

        private string wordToGuess = "";

        private KeyCode lastKeyPressed;

        
        private void Start()
        {
            var randomIndex = Random.Range(0, words.Length);

            wordToGuess = words[randomIndex];

            for (var i = 0; i < wordToGuess.Length; i++)
            {
                initialWord += "_";
            }

            _fieldText.text = initialWord;
            _hpText.text = "Hp left =" + hp.ToString();

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

            char pressedKeyString = key.ToString()[0];
            string wordUppercase = wordToGuess.ToUpper();
            
            bool wordContainsPressedKey = wordUppercase.Contains(pressedKeyString);
            bool letterWasGuessed = guessedLetters.Contains(pressedKeyString);

            if (!wordContainsPressedKey && !wrongTriedLetter.Contains(pressedKeyString))
            {
                wrongTriedLetter.Add(pressedKeyString);
                hp -= 1;

                if (hp <= 0)
                {
                    gameManagerLose.gameOverLose();
                    print("Lose");
                }
                else
                {
                    _hpText.text = "Hp left =" + hp.ToString();
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
                gameManagerWin.gameOverWin();
                print("Win");
            }
            
            print(stringToPrint);
            _fieldText.text = stringToPrint;




        }
}

