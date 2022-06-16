using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagement : MonoBehaviour
{
    static int currSceneIdx = 0;
    public static void ToMainMenu()
    {
        currSceneIdx = 0;
        SceneManager.LoadScene(currSceneIdx);
    }
    public static void PrevScene()
    {
        currSceneIdx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currSceneIdx - 1);
    }
    public static void Restart()
    {
        currSceneIdx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currSceneIdx);
    }
    public static void NextScene()
    {
        currSceneIdx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currSceneIdx + 1);
    }
}