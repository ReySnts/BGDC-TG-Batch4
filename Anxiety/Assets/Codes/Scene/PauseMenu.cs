using UnityEngine;
using System.Collections;
public class PauseMenu : MonoBehaviour
{
    public static PauseMenu objInstance = null;
    [SerializeField] bool isPaused = false;
    [Header("References")]
    public GameObject pauseMenu = null;
    public GameObject health = null;
    public GameObject sounds = null;
    public AudioSource ambience = null;
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
            try
            {
                health.SetActive(true);
                LightCursor.objInstance.enabled = true;
            }
            catch
            {
                health = null;
                LightCursor.objInstance = null;
            }
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
    void CheckIfThereIsFearObject()
    {
        if (Fear.objInstance != null) Fear.willShowHealth = true;
        else Fear.willShowHealth = false;
    }
    IEnumerator HoldStart()
    {
        yield return new WaitForEndOfFrame();
        CheckIfThereIsFearObject();
        Continue();
    }
    void Awake()
    {
        try
        {
            if (
                objInstance == null && 
                SceneManagement.GetCurrentScene() != 0 &&
                SceneManagement.GetCurrentScene() != 5
            )
            {
                objInstance = this;
                transform.parent = null;
            }
            else if (objInstance != this) Destroy(gameObject);
            StartCoroutine(HoldStart());
        }
        catch
        {
            health = null;
        }
    }
    void Pause()
    {
        pauseMenu.SetActive(true);
        try
        {
            LightCursor.objInstance.enabled = false;
            health.SetActive(false);
        }
        catch
        {
            LightCursor.objInstance = null;
            health = null;
        }
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