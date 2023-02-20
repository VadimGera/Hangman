using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class MenuControl : MonoBehaviour
    {
        [SerializeField] private string sceneName;


        public void PlayPressed()
        {
            SceneManager.LoadSceneAsync(sceneName);
        }

        public void PressedExit()
        {
            SceneManager.LoadSceneAsync(sceneName);
        }

        public void ExitPressed()
        {
           Application.Quit();
        }
    }
}