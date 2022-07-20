using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Tutorial : MonoBehaviour
{
    int dialogueIdx = 0;
    float waitTime = 2f;
    bool onButtonPress = false;
    public static bool willShowHealth = false;
    [Header("References")]
    public GameObject health = null;
    public GameObject[] roomLights = new GameObject[2];
    PauseMenu pauseMenu = null;
    Dialogue dialogue = null;
    PlayerMovement playerMovement = null;
    LightCursor lightCursor = null;
    Door tutorialDoor = null;
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
        dialBoxAnimControl.SetBool("IsClosed", pauseMenu.enabled = true);
        pressEnterAnimControl.SetBool("EndedDialogue", playerMovement.enabled = endDialogue = true);
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
    void SetDialogue()
    {
        dialogue = FindObjectOfType<Dialogue>();
        dialogueStorage.Clear();
        foreach (string sentence in dialogue.sentences) dialogueStorage.Enqueue(sentence);
        StartDialogue();
    }
    void SetGuideline()
    {
        health.SetActive(false);
        (lightCursor = FindObjectOfType<LightCursor>()).enabled = false;
        guideline = FindObjectOfType<Guideline>();
        foreach (GameObject roomLight in roomLights) roomLight.SetActive(true);
        foreach (string sentence in guideline.sentences) guidelineStorage.Enqueue(sentence);
        guidance.text = guidelineStorage.Dequeue();
    }
    void DisableSomeObjects()
    {
        (pauseMenu = FindObjectOfType<PauseMenu>()).enabled = 
            (playerMovement = FindObjectOfType<PlayerMovement>()).enabled = 
                (tutorialDoor = FindObjectOfType<Door>()).enabled = 
                    false;
    }
    void Start()
    {
        DisableSomeObjects();
        SetDialogue();
        SetGuideline();
    }
    void PlayGuideAnim()
    {
        switch (guidelineStorage.Count)
        {
            case 4:
                {
                    willShowHealth = true;
                    health.SetActive(true);
                    fearMeterAnimControl.Play("FearMeterFading");
                    break;
                }
            case 3:
                {
                    willShowHealth = false;
                    health.SetActive(false);
                    foreach (GameObject roomLight in roomLights) roomLight.SetActive(false);
                    lightCursor.enabled = true;
                    lightCursorFadingAnimControl.Play("LightCursorFading");
                    break;
                }
            case 2:
                {
                    lightCursorFadingAnimControl.SetBool("IsStopFading", true);
                    mushroomGlowingAnimControl.SetBool("IsMushroomGlowing", true);
                    break;
                }
            case 1:
                {
                    mushroomGlowingAnimControl.SetBool("IsMushroomGlowing", false);
                    tutorialDoor.enabled = true;
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