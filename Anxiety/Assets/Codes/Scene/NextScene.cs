using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class NextScene : MonoBehaviour
{
    public static NextScene objInstance = null;
    string colliderName = "Player";
    bool isTriggered = false;
    [Header("Loading")]
    GameObject loadingLevel = null;
    GameObject loadingMainMenu = null;
    GameObject loadingMeter = null;
    GameObject sounds = null;
    GameObject pauseMenu = null;
    GameObject tutorial = null;
    public Slider loadingSlider = null;
    Canvas loadingCanvas = null;
    void Awake()
    {
        if (objInstance == null && SceneManagement.GetCurrentScene() != 5) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
    }
    void OnEnable()
    {
        SetLoading();
        sounds = GameObject.Find("Sounds");
        (loadingLevel = GameObject.Find("LoadingLevel")).SetActive(false);
        try
        {
            (loadingMainMenu = GameObject.Find("LoadingMainMenu")).SetActive(false);
            pauseMenu = GameObject.Find("PauseMenu");
        }
        catch
        {
            loadingMainMenu = pauseMenu = null;
        }
        try
        {
            tutorial = GameObject.Find("Tutorial");
        }
        catch
        {
            tutorial = null;
        }
    }
    void SetLoading()
    {
        loadingSlider.minValue = 0f;
        loadingSlider.maxValue = 14f;
        loadingSlider.wholeNumbers = true;
        try
        {
            loadingCanvas = GameObject.Find("Loading").GetComponent<Canvas>();
            loadingCanvas.sortingOrder = 1;
        }
        catch
        {
            loadingCanvas = null;
        }
        try
        {
            (loadingMeter = GameObject.Find("LoadingMeter")).SetActive(false);
        }
        catch
        {
            loadingMeter = null;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.name == colliderName)
        {
            isTriggered = true;
            StartLoading("Level");
        }
    }
    public void QuitGame()
    {
        StartLoading("MainMenu");
    }
    public void StartLoading(string loadingName)
    {
        try
        {
            tutorial.SetActive(false);
        }
        catch
        {
            tutorial = null;
        }
        try
        {
            PauseMenu.objInstance.enabled = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            loadingMeter.SetActive(true);
        }
        catch
        {
            PauseMenu.objInstance = null;
            pauseMenu = loadingMeter = null;
        }
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
            case "MainMenu":
                SceneManagement.ToMainMenu();
                break;
        }
    }
}