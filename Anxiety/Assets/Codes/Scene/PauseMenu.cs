using UnityEngine;
using System.Collections;
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
        if (Fear.objInstance != null) Fear.willShowHealth = true;
        else Fear.willShowHealth = false;
    }
    IEnumerator HoldStart()
    {
        yield return new WaitForEndOfFrame();
        lightCursor = FindObjectOfType<LightCursor>();
        CheckIfThereIsFearObject();
        Continue();
    }
    void Awake()
    {
        StartCoroutine(HoldStart());
    }
    void CheckIfLevelUseHealth()
    {
        if (Tutorial.objInstance != null)
        {
            if (Tutorial.objInstance.isFadingFearMeter) Tutorial.objInstance.IntroduceFearMeter();
            else
            {
                Tutorial.objInstance.HideFearMeter();
                if (Tutorial.objInstance.turnOnLightCursor) Tutorial.objInstance.IntroduceLightCursor();
                else
                {
                    Tutorial.objInstance.HideLightCursor();
                    if (Tutorial.objInstance.hasTurnedOnLightCursor) Tutorial.objInstance.UnlockLightCursor();
                }
            }
        }
        else
        {
            health.SetActive(true);
            lightCursor.enabled = true;
        }
    }
    void Continue()
    {
        pauseMenu.SetActive(false);
        ambience.Stop();
        sounds.SetActive(true);
        CheckIfLevelUseHealth();
        Time.timeScale = 1f;
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