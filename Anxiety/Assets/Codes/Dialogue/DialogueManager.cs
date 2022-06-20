using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    static int dialogueIdx = 0;
    Queue<string> sentenceStorage = new Queue<string>();
    public Dialogue dialogue = null;
    [Header("Texts")]
    public TextMeshProUGUI characterName = null;
    public TextMeshProUGUI dialogueSentence = null;
    void Start()
    {
        dialogueIdx = 0;
        dialogue = FindObjectOfType<Dialogue>();
        sentenceStorage.Clear();
        foreach (string sentece in dialogue.sentences) sentenceStorage.Enqueue(sentece);
    }
    void StartDialogue()
    {
        if (sentenceStorage.Count > 0)
        {
            characterName.text = (dialogueIdx % 2 == 0) ? 
                dialogue.characterName[0] : 
                dialogue.characterName[1];
            dialogueSentence.text = sentenceStorage.Dequeue();
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartDialogue();
            dialogueIdx++;
        }
    }
}