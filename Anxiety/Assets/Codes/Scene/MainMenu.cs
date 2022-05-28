using UnityEngine;
public class MainMenu : MonoBehaviour
{
    public SceneManagement sceneManagement = null;
    void Start()
    {
        sceneManagement = FindObjectOfType<SceneManagement>();
    }
    public void Play()
    {
        sceneManagement.NextScene();
    }
    public void Exit()
    {
        Application.Quit();
    }
}