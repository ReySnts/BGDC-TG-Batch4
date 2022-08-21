using System.Collections;
using UnityEngine;
public class Ending : StartLevel
{
    public static Ending objInstance = null;
    public Animator backgroundAnimControl = null;
    bool isTriggered = false;
    string colliderName = "Player";
    void Awake()
    {
        if (objInstance == null && SceneManagement.GetCurrentScene() == 4) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
    }
    void OnEnable()
    {
        try
        {
            DisableSomeObjects();
            SetDialogue();
            SetGuideline();
        }
        catch { }
    }
    IEnumerator HoldCredits()
    {
        yield return new WaitForSeconds(5f);
        NextScene.objInstance.StartLoading("Level");
    }
    void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.name == colliderName)
        {
            isTriggered = true;
            backgroundAnimControl.SetBool("IsFadeIn", true);
            StartCoroutine(HoldCredits());
        }
    }
}