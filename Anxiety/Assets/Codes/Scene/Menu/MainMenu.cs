using UnityEngine;
public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManagement.NextScene();
    }
    public void Credits()
    {
        SceneManagement.ToCredits();
    }
    public void Exit()
    {
        Application.Quit();
    }
}