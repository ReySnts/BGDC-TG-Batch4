using UnityEngine;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] bool isPaused = false;
    [Header("References")]
    public GameObject pauseMenu = null;
    public GameObject health = null;
    public GameObject sounds = null;
    void Awake()
    {
        Continue();
    }
    void Pause()
    {
        pauseMenu.SetActive(true);
        health.SetActive(false);
        sounds.SetActive(false);
        Time.timeScale = 0f;
    }
    void Continue()
    {
        pauseMenu.SetActive(false);
        health.SetActive(true);
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