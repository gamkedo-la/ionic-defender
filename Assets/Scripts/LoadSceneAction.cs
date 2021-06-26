using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAction : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
