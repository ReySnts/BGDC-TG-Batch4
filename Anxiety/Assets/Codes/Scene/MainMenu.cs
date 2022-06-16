using UnityEngine;
public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManagement.NextScene();
    }
    public void Exit()
    {
        Application.Quit();
    }
}