using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class NextScene : MonoBehaviour
{
    public static NextScene objInstance = null;
    string colliderName = "Player";
    bool isTriggered = false;
    [Header("Loading")]
    GameObject loadingBackground = null;
    GameObject loadingLevel = null;
    GameObject loadingRestart = null;
    GameObject loadingMainMenu = null;
    GameObject loadingMeter = null;
    GameObject sounds = null;
    GameObject pauseMenu = null;
    GameObject tutorial = null;
    [Header("Loading UI")]
    Canvas loadingCanvas = null;
    Slider loadingSlider = null;
    void Awake()
    {
        if (objInstance == null && SceneManagement.GetCurrentScene() != 5) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
    }
    void OnEnable()
    {
        #region Loading Slider
        loadingSlider = GameObject.Find("LoadingSlider").GetComponent<Slider>();
        loadingSlider.minValue = 0f;
        loadingSlider.maxValue = 14f;
        loadingSlider.wholeNumbers = true;
        #endregion
        #region Tutorial
        try
        {
            tutorial = GameObject.Find("Tutorial");
        }
        catch
        {
            tutorial = null;
        }
        #endregion
        #region Pause Menu
        try
        {
            loadingCanvas = GameObject.Find("Loading").GetComponent<Canvas>();
            loadingCanvas.sortingOrder = 1;
            (loadingBackground = GameObject.Find("LoadingBackground")).SetActive(false);
            (loadingRestart = GameObject.Find("LoadingRestart")).SetActive(false);
            (loadingMainMenu = GameObject.Find("LoadingMainMenu")).SetActive(false);
            (loadingMeter = GameObject.Find("LoadingMeter")).SetActive(false);
            pauseMenu = GameObject.Find("PauseMenu");
        }
        catch
        {
            loadingCanvas = null;
            loadingBackground = loadingRestart = loadingMainMenu = loadingMeter = pauseMenu = null;
        }
        #endregion
        sounds = GameObject.Find("Sounds");
        (loadingLevel = GameObject.Find("LoadingLevel")).SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.name == colliderName)
        {
            isTriggered = true;
            StartLoading("Level");
        }
    }
    public void RestartLevel()
    {
        StartLoading("Restart");
    }
    public void ExitLevel()
    {
        StartLoading("MainMenu");
    }
    public void StartLoading(string loadingName)
    {
        #region Tutorial
        try
        {
            tutorial.SetActive(false);
        }
        catch
        {
            tutorial = null;
        }
        #endregion
        #region Pause Menu
        try
        {
            PauseMenu.objInstance.enabled = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            loadingBackground.SetActive(true);
            loadingMeter.SetActive(true);
        }
        catch
        {
            PauseMenu.objInstance = null;
            pauseMenu = loadingBackground = loadingMeter = null;
        }
        #endregion
        TurnOnUI(loadingName);
        sounds.SetActive(false);
        StartCoroutine(Loading(loadingName, 2f));
    }
    void TurnOnUI(string loadingName)
    {
        switch (loadingName)
        {
            case "Level":
                loadingLevel.SetActive(true);
                break;
            case "Restart":
                loadingRestart.SetActive(true);
                break;
            case "MainMenu":
                loadingMainMenu.SetActive(true);
                break;
        }
    }
    IEnumerator Loading(string loadingName, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        loadingSlider.value += waitTime;
        switch (waitTime)
        {
            case 2f:
                StartCoroutine(Loading(loadingName, 8f));
                break;
            case 8f:
                StartCoroutine(Loading(loadingName, 3f));
                break;
            case 3f:
                StartCoroutine(Loading(loadingName, 1f));
                break;
            case 1f:
                LoadScene(loadingName);
                break;
        }
    }
    void LoadScene(string loadingName)
    {
        switch (loadingName)
        {
            case "Level":
                SceneManagement.NextScene();
                break;
            case "Restart":
                SceneManagement.Restart();
                break;
            case "MainMenu":
                SceneManagement.ToMainMenu();
                break;
        }
    }
}