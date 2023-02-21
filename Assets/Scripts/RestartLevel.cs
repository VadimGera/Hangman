using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    [SerializeField] private string sceneName;
    public void Replay()
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
