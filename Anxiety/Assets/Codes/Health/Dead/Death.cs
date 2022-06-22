using System.Collections;
using UnityEngine;
public abstract class Death : MonoBehaviour
{
    Fear fear = null;
    protected string colliderName = "Player";
    protected bool isTriggered = false;
    [Header("References")]
    public Animator playerControlAnim = null;
    public AudioSource dieSound = null;
    protected abstract void SetAnim();
    IEnumerator GenerateTime()
    {
        yield return new WaitForSeconds(6f);
        SceneManagement.Restart();
    }
    void Start()
    {
        fear = FindObjectOfType<Fear>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.name == colliderName)
        {
            fear.fearMeter.value = fear.maxFearValue;
            SetAnim();
            dieSound.Play();
            StartCoroutine(GenerateTime());
            isTriggered = true;
        }
    }
}