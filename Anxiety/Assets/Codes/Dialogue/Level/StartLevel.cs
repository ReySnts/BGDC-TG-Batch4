using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StartLevel : MonoBehaviour
{
    public GameObject guideline = null;
    [Header("Values")]
    bool onButtonPress = false;
    int dialogueIdx = 0;
    protected float waitTime = 2f;
    [Header("Dialogue")]
    readonly Queue<string> dialogueStorage = new Queue<string>();
    protected bool endDialogue = false;
    [Header("Guideline")]
    protected Queue<string> guidelineStorage = new Queue<string>();
    protected bool onGuideHeld = false;
    [Header("Animators")]
    public Animator dialBoxAnimControl = null;
    public Animator pressEnterAnimControl = null;
    [Header("Texts")]
    public TextMeshProUGUI characterName = null;
    public TextMeshProUGUI dialogueSentence = null;
    public TextMeshProUGUI guidance = null;
    protected virtual void DisableOtherObjects() { }
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
        try
        {
            guidance.text = guidelineStorage.Dequeue();
        }
        catch
        {
            guideline.SetActive(false);
        }
    }
    protected void StartDialogue()
    {
        if (dialogueStorage.Count > 0)
        {
            characterName.text = (dialogueIdx % 2 == 0) ? Dialogue.objInstance.characterName[0] : Dialogue.objInstance.characterName[1];
            StartCoroutine(AnimateEachLetter(dialogueStorage.Dequeue()));
        }
        else EndDialogue();
    }
    protected void SetGuideline()
    {
        foreach (string sentence in Guideline.objInstance.sentences) guidelineStorage.Enqueue(sentence);
        guidance.text = guidelineStorage.Dequeue();
    }
    protected void SetDialogue()
    {
        dialogueStorage.Clear();
        foreach (string sentence in Dialogue.objInstance.sentences) dialogueStorage.Enqueue(sentence);
        StartDialogue();
    }
    protected void DisableSomeObjects()
    {
        PauseMenu.objInstance.enabled = 
        PlayerMovement.objInstance.enabled = false;
        DisableOtherObjects();
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