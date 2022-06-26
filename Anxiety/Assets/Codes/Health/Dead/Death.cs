using UnityEngine;
public abstract class Death : MonoBehaviour
{
    Fear fear = null;
    public AudioSource dieAfterJumpSound = null;
    protected string colliderName = "Player";
    protected float restartTime = 0f;
    protected bool isTriggered = false;
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
            StartCoroutine(fear.HoldRestart(restartTime));
            fear.DieAfterJump();
            dieAfterJumpSound.Play();
            isTriggered = true;
        }
    }
}