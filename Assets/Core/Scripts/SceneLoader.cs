using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadSceneId(int id)
    {
        SceneManager.LoadScene(id);
    }

    public void LoadLevelId()
    {
        SceneManager.LoadScene(Saves.Level);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
