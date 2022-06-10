using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagement : MonoBehaviour
{
    [SerializeField] int currSceneIdx = 0;
    public void ToMainMenu()
    {
        currSceneIdx = 0;
        SceneManager.LoadScene(currSceneIdx);
    }
    public void PrevScene()
    {
        currSceneIdx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currSceneIdx - 1);
    }
    public void Restart()
    {
        currSceneIdx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currSceneIdx);
    }
    public void NextScene()
    {
        currSceneIdx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currSceneIdx + 1);
    }
}