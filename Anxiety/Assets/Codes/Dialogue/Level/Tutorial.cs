using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Tutorial : StartLevel
{
    public static Tutorial objInstance = null;
    [Header("References")]
    public GameObject health = null;
    public GameObject[] roomLights = new GameObject[2];
    public Slider fearMeterFill = null;
    [Header("Values")]
    public bool isFadingFearMeter = false;
    public bool turnOnLightCursor = false;
    public bool hasTurnedOnLightCursor = false;
    Door tutorialDoor = null;
    [Header("Animators")]
    public Animator fearMeterAnimControl = null;
    public Animator lightCursorFadingAnimControl = null;
    public Animator mushroomGlowingAnimControl = null;

    protected override void DisableOtherObjects()
    {
        LightCursor.objInstance.enabled = 
        CheckPoint.objInstance.enabled = 
        (tutorialDoor = FindObjectOfType<Door>()).enabled = 
        fearMeterFill.interactable = false;
        fearMeterFill.fillRect = null;
        health.SetActive(false);
        foreach (GameObject roomLight in roomLights) roomLight.SetActive(true);
    }
    void Awake()
    {
        if (objInstance == null && SceneManagement.GetCurrentScene() == 1) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
    }
    void Start()
    {
        try
        {
            DisableSomeObjects();
            SetDialogue();
            SetGuideline();
        }
        catch { }
    }
    public void UnlockLightCursor()
    {
        lightCursorFadingAnimControl.SetBool("HasTurnedOnLightCursor", hasTurnedOnLightCursor);
        LightCursor.objInstance.enabled = true;
    }
    public void HideLightCursor()
    {
        lightCursorFadingAnimControl.SetBool("IsLightCursorFading", false);
        lightCursorFadingAnimControl.SetBool("HasTurnedOnLightCursor", hasTurnedOnLightCursor);
        LightCursor.objInstance.enabled = false;
    }
    public void IntroduceLightCursor()
    {
        LightCursor.objInstance.enabled = true;
        lightCursorFadingAnimControl.SetBool("IsLightCursorFading", true);
    }
    public void HideFearMeter()
    {
        fearMeterAnimControl.SetBool("IsFearMeterFading", false);
        health.SetActive(false);
    }
    public void IntroduceFearMeter()
    {
        health.SetActive(true);
        fearMeterAnimControl.SetBool("IsFearMeterFading", true);
    }
    void PlayGuideAnim()
    {
        switch (guidelineStorage.Count)
        {
            case 4:
                {
                    isFadingFearMeter = true;
                    IntroduceFearMeter();
                    break;
                }
            case 3:
                {
                    isFadingFearMeter = false;
                    HideFearMeter();
                    foreach (GameObject roomLight in roomLights) roomLight.SetActive(false);
                    turnOnLightCursor = true;
                    IntroduceLightCursor();
                    break;
                }
            case 2:
                {
                    turnOnLightCursor = false;
                    HideLightCursor();
                    hasTurnedOnLightCursor = true;
                    UnlockLightCursor();
                    mushroomGlowingAnimControl.SetBool("IsCheckpointGlowing", true);
                    break;
                }
            case 1:
                {
                    mushroomGlowingAnimControl.SetBool("IsCheckpointGlowing", false);
                    tutorialDoor.enabled = CheckPoint.objInstance.enabled = true;
                    break;
                }
            default:
                break;
        }
    }
    void DisplayEachGuidance()
    {
        onGuideHeld = false;
        PlayGuideAnim();
        guidance.text = guidelineStorage.Dequeue();
    }
    IEnumerator HoldEachGuidance()
    {
        onGuideHeld = true;
        yield return new WaitForSeconds(waitTime * 5f);
        DisplayEachGuidance();
    }
    void LateUpdate()
    {
        if (endDialogue && !onGuideHeld)
        {
            if (guidelineStorage.Count > 0) StartCoroutine(HoldEachGuidance());
            else onGuideHeld = true;
        }
    }
}