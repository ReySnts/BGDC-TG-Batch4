using UnityEngine;
public abstract class Death : MonoBehaviour
{
    Fear fear = null;
    public AudioSource dieAfterJumpSound = null;
    public bool isTriggered = false;
    protected string colliderName = "Player";
    protected float restartTime = 0f;
    protected abstract void SetRestartTime();
    void Start()
    {
        fear = FindObjectOfType<Fear>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.name == colliderName)
        {
            SetRestartTime();
            fear.DieAfterJump();
            dieAfterJumpSound.Play();
            isTriggered = true;
            StartCoroutine(fear.FadeIn(restartTime));
        }
    }
}