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
    public static void ToCredits()
    {
        currSceneIdx = 5;
        SceneManager.LoadScene(currSceneIdx);
    }
    public static int GetCurrentScene()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
    public static void PrevScene()
    {
        currSceneIdx = GetCurrentScene();
        SceneManager.LoadScene(currSceneIdx - 1);
    }
    public static void Restart()
    {
        currSceneIdx = GetCurrentScene();
        SceneManager.LoadScene(currSceneIdx);
    }
    public static void NextScene()
    {
        currSceneIdx = GetCurrentScene();
        SceneManager.LoadScene(currSceneIdx + 1);
    }
}