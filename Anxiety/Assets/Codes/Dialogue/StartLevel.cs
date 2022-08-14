using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public abstract class StartLevel : MonoBehaviour
{
    Dialogue dialogue = null;
    [Header("Values")]
    bool onButtonPress = false;
    int dialogueIdx = 0;
    protected float waitTime = 2f;
    [Header("Dialogue")]
    Queue<string> dialogueStorage = new Queue<string>();
    protected bool endDialogue = false;
    [Header("Guideline")]
    protected Queue<string> guidelineStorage = new Queue<string>();
    Guideline guideline = null;
    protected bool onGuideHeld = false;
    [Header("Animators")]
    public Animator dialBoxAnimControl = null;
    public Animator pressEnterAnimControl = null;
    [Header("Texts")]
    public TextMeshProUGUI characterName = null;
    public TextMeshProUGUI dialogueSentence = null;
    public TextMeshProUGUI guidance = null;
    protected abstract void SetUp();
    protected IEnumerator AnimateEachLetter(string sentence)
    {
        dialogueSentence.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueSentence.text += letter;
            yield return null;
        }
    }
    protected void EndDialogue()
    {
        dialBoxAnimControl.SetBool("IsClosed", PauseMenu.objInstance.enabled = true);
        pressEnterAnimControl.SetBool("EndedDialogue", PlayerMovement.objInstance.enabled = endDialogue = true);
        guidance.text = guidelineStorage.Dequeue();
    }
    protected void StartDialogue()
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
    }
    protected void SetDialogue()
    {
        dialogue = FindObjectOfType<Dialogue>();
        dialogueStorage.Clear();
        foreach (string sentence in dialogue.sentences) dialogueStorage.Enqueue(sentence);
        StartDialogue();
    }
    protected void DisableSomeObjects()
    {
        PauseMenu.objInstance.enabled = // Still Error because StartSceneDoor.
        PlayerMovement.objInstance.enabled = false;
        SetUp();
    }
    protected void Start()
    {
        DisableSomeObjects();
        SetDialogue();
        SetGuideline();
    }
    protected IEnumerator HoldPress()
    {
        onButtonPress = true;
        yield return new WaitForSeconds(waitTime);
        onButtonPress = false;
    }
    protected void Update()
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
    }
}