using UnityEngine;
using System.Collections;
public class PauseMenu : MonoBehaviour
{
    public static PauseMenu objInstance = null;
    [SerializeField] bool isPaused = false;
    [Header("References")]
    GameObject pauseMenu = null;
    GameObject sounds = null;
    [Header("Pause UI")]
    GameObject pause = null;
    GameObject settings = null;
    [Header("Ask Player")]
    GameObject askPlayer = null;
    GameObject restartLevel = null;
    GameObject exitLevel = null;
    void CheckIfLevelUseHealth()
    {
        try
        {
            #region Tutorial
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
            #endregion
        }
        catch
        {
            Tutorial.objInstance = null;
            #region Enable Light Cursor
            try
            {
                LightCursor.objInstance.enabled = true;
            }
            catch
            {
                LightCursor.objInstance = null;
            }
            #endregion
            #region Enable Health
            try
            {
                Fear.objInstance.enabled = true;
            }
            catch
            {
                Fear.objInstance = null;
            }
            #endregion
        }
    }
    public void Continue()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        sounds.SetActive(true);
        CheckIfLevelUseHealth();
        Time.timeScale = 1f;
    }
    IEnumerator HoldFrameAfterAwake()
    {
        yield return new WaitForEndOfFrame();
        #region Register Game Object
        pauseMenu = GameObject.Find("PauseMenu");
        sounds = GameObject.Find("Sounds");
        pause = GameObject.Find("Pause");
        settings = GameObject.Find("Settings");
        askPlayer = GameObject.Find("AskPlayer");
        restartLevel = GameObject.Find("RestartLevel");
        exitLevel = GameObject.Find("ExitLevel");
        #endregion
        #region Check If There Is Fear Object
        if (Fear.objInstance != null) Fear.willShowHealth = true;
        else Fear.willShowHealth = false;
        #endregion
        Continue();
    }
    void Awake()
    {
        if (
            objInstance == null &&
            SceneManagement.GetCurrentScene() != 0 &&
            SceneManagement.GetCurrentScene() != 5
        )
        {
            objInstance = this;
            StartCoroutine(HoldFrameAfterAwake());
        }
        else if (objInstance != this) Destroy(gameObject);
    }
    void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        OpenPause();
        #region Disable Health
        try
        {
            Fear.objInstance.enabled = false;
        }
        catch
        {
            Fear.objInstance = null;
        }
        #endregion
        #region Disable Light Cursor
        try
        {
            LightCursor.objInstance.enabled = false;
        }
        catch
        {
            LightCursor.objInstance = null;
        }
        #endregion
        sounds.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused) Pause();
            else Continue();
        }
    }
    public void OpenPause()
    {
        settings.SetActive(false);
        askPlayer.SetActive(false);
        pause.SetActive(true);
    }
    public void OpenSettings()
    {
        pause.SetActive(false);
        askPlayer.SetActive(false);
        settings.SetActive(true);
    }
    public void AskPlayer(string askName)
    {
        pause.SetActive(false);
        settings.SetActive(false);
        askPlayer.SetActive(true);
        switch (askName)
        {
            case "RestartLevel":
                AskToRestartLevel();
                break;
            case "ExitLevel":
                AskToExitLevel();
                break;
        }
    }
    void AskToRestartLevel()
    {
        exitLevel.SetActive(false);
        restartLevel.SetActive(true);
    }
    void AskToExitLevel()
    {
        restartLevel.SetActive(false);
        exitLevel.SetActive(true);
    }
}