using UnityEngine;
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu = null;
    public GameObject sounds = null;
    [SerializeField] bool isPaused = false;
    void Awake()
    {
        Continue();
    }
    void Pause()
    {
        pauseMenu.SetActive(true);
        sounds.SetActive(false);
        Time.timeScale = 0f;
    }
    void Continue()
    {
        pauseMenu.SetActive(false);
        sounds.SetActive(true);
        Time.timeScale = 1f;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Pause();
                isPaused = true;
            }
            else
            {
                Continue();
                isPaused = false;
            }
        }
    }
}