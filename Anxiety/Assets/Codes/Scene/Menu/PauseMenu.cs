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
    public GameObject pauseUI = null;
    public GameObject settingsUI = null;
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
        CloseSettings();
        pauseMenu.SetActive(false);
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
                StartCoroutine(HoldStart());
            }
            else if (objInstance != this) Destroy(gameObject);
        }
        catch
        {
            health = null;
        }
    }
    void Pause()
    {
        pauseMenu.SetActive(true);
        CloseSettings();
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
    public void OpenSettings()
    {
        settingsUI.SetActive(true);
        pauseUI.SetActive(false);
    }
    public void CloseSettings()
    {
        pauseUI.SetActive(true);
        settingsUI.SetActive(false);
    }
}