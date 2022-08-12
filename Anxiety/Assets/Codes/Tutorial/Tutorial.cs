using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Tutorial : MonoBehaviour
{
    public static Tutorial objInstance = null;
    [Header("References")]
    public GameObject health = null;
    public GameObject[] roomLights = new GameObject[2];
    public Slider fearMeterFill = null;
    Dialogue dialogue = null;
    Door tutorialDoor = null;
    [Header("Values")]
    public bool isFadingFearMeter = false;
    public bool turnOnLightCursor = false;
    public bool hasTurnedOnLightCursor = false;
    bool onButtonPress = false;
    int dialogueIdx = 0;
    float waitTime = 2f;
    [Header("Dialogue")]
    Queue<string> dialogueStorage = new Queue<string>();
    bool endDialogue = false;
    [Header("Guideline")]
    Queue<string> guidelineStorage = new Queue<string>();
    Guideline guideline = null;
    bool onGuideHeld = false;
    [Header("Animators")]
    public Animator dialBoxAnimControl = null;
    public Animator pressEnterAnimControl = null;
    public Animator fearMeterAnimControl = null;
    public Animator lightCursorFadingAnimControl = null;
    public Animator mushroomGlowingAnimControl = null;
    [Header("Texts")]
    public TextMeshProUGUI characterName = null;
    public TextMeshProUGUI dialogueSentence = null;
    public TextMeshProUGUI guidance = null;
    void Awake()
    {
        if (objInstance == null && SceneManagement.GetCurrentScene() == 1)
        {
            objInstance = this;
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }
        else if (objInstance != this) Destroy(gameObject);
    }
    IEnumerator AnimateEachLetter(string sentence)
    {
        dialogueSentence.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueSentence.text += letter;
            yield return null;
        }
    }
    void EndDialogue()
    {
        dialBoxAnimControl.SetBool("IsClosed", PauseMenu.objInstance.enabled = true);
        pressEnterAnimControl.SetBool("EndedDialogue", PlayerMovement.objInstance.enabled = endDialogue = true);
        guidance.text = guidelineStorage.Dequeue();
    }
    void StartDialogue()
    {
        if (dialogueStorage.Count > 0)
        {
            characterName.text = (dialogueIdx % 2 == 0) ? dialogue.characterName[0] : dialogue.characterName[1];
            StartCoroutine(AnimateEachLetter(dialogueStorage.Dequeue()));
        }
        else EndDialogue();
    }
    void SetGuideline()
    {
        guideline = FindObjectOfType<Guideline>();
        foreach (string sentence in guideline.sentences) guidelineStorage.Enqueue(sentence);
        guidance.text = guidelineStorage.Dequeue();
        fearMeterFill.interactable = false;
        fearMeterFill.fillRect = null;
        health.SetActive(false);
        foreach (GameObject roomLight in roomLights) roomLight.SetActive(true);
    }
    void SetDialogue()
    {
        dialogue = FindObjectOfType<Dialogue>();
        dialogueStorage.Clear();
        foreach (string sentence in dialogue.sentences) dialogueStorage.Enqueue(sentence);
        StartDialogue();
    }
    void DisableSomeObjects()
    {
        PauseMenu.objInstance.enabled = 
        PlayerMovement.objInstance.enabled = 
        LightCursor.objInstance.enabled = 
        CheckPoint.objInstance.enabled =
        (tutorialDoor = FindObjectOfType<Door>()).enabled = false;
    }
    void Start()
    {
        DisableSomeObjects();
        SetDialogue();
        SetGuideline();
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
    IEnumerator HoldPress()
    {
        onButtonPress = true;
        yield return new WaitForSeconds(waitTime);
        onButtonPress = false;
    }
    void Update()
    {
        if (!endDialogue)
        {
            if (!onButtonPress)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    StartCoroutine(HoldPress());
                    dialogueIdx++;
                    StartDialogue();
                }
                else if (Input.GetKeyDown(KeyCode.Escape))
                {
                    StartCoroutine(HoldPress());
                    EndDialogue();
                }
            }
        }
        else if (!onGuideHeld)
        {
            if (guidelineStorage.Count > 0) StartCoroutine(HoldEachGuidance());
            else onGuideHeld = true;
        }
    }
}