using UnityEngine;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] bool isPaused = false;
    [Header("References")]
    public GameObject pauseMenu = null;
    public GameObject health = null;
    public GameObject sounds = null;
    public LightCursor lightCursor = null;
    public AudioSource ambience = null;
    void CheckIfThereIsFearObject()
    {
        if (FindObjectOfType<Fear>() != null) Fear.willShowHealth = true;
        else Fear.willShowHealth = false;
    }
    void Awake()
    {
        lightCursor = FindObjectOfType<LightCursor>();
        CheckIfThereIsFearObject();
        Continue();
    }
    void Pause()
    {
        pauseMenu.SetActive(true);
        lightCursor.enabled = false;
        health.SetActive(false);
        sounds.SetActive(false);
        ambience.Play();
        Time.timeScale = 0f;
    }
    void CheckIfLevelUseHealth()
    {
        if (Tutorial.willShowHealth || Fear.willShowHealth) health.SetActive(true);
        else health.SetActive(false);
    }
    void Continue()
    {
        pauseMenu.SetActive(false);
        ambience.Stop();
        lightCursor.enabled = true;
        CheckIfLevelUseHealth();
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