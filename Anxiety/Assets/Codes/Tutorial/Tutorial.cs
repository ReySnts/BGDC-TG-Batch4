using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Tutorial : MonoBehaviour
{
    int dialogueIdx = 0;
    bool onPress = false;
    bool endDialogue = false;
    bool onHeld = false;
    PauseMenu pauseMenu = null;
    Dialogue dialogue = null;
    PlayerMovement playerMovement = null;
    Queue<string> dialogueStorage = new Queue<string>();
    Queue<string> guidelineStorage = new Queue<string>();
    Guideline guideline = null;
    [Header("Animators")]
    public Animator dialBoxAnimControl = null;
    public Animator pressEnterAnimControl = null;
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
        guideline = FindObjectOfType<Guideline>();
        foreach (string sentence in guideline.sentences) guidelineStorage.Enqueue(sentence);
        guidance.text = guidelineStorage.Dequeue();
    }
    void Start()
    {
        (pauseMenu = FindObjectOfType<PauseMenu>()).enabled = (playerMovement = FindObjectOfType<PlayerMovement>()).enabled = false;
        SetDialogue();
        SetGuideline();
    }
    IEnumerator HoldEachGuidance()
    {
        onHeld = true;
        yield return new WaitForSeconds(5f);
        onHeld = false;
        guidance.text = guidelineStorage.Dequeue();
    }
    void EndDialogue()
    {
        dialBoxAnimControl.SetBool("IsClosed", pauseMenu.enabled = true);
        pressEnterAnimControl.SetBool("EndedDialogue", playerMovement.enabled = endDialogue = true);
        guidance.text = guidelineStorage.Dequeue();
        //StartCoroutine(HoldEachGuidance());
    }
    IEnumerator HoldPress()
    {
        onPress = true;
        yield return new WaitForSeconds(2f);
        onPress = false;
    }
    void Update()
    {
        if (!endDialogue && !onPress)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape))
            {
                StartCoroutine(HoldPress());
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    dialogueIdx++;
                    StartDialogue();
                }
                else EndDialogue();
            }
        }
    }
}