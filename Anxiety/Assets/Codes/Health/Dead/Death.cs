using UnityEngine;
public abstract class Death : MonoBehaviour
{
    public AudioSource dieAfterJumpSound = null;
    public bool isTriggered = false;
    protected string colliderName = "Player";
    protected float restartTime = 0f;
    protected abstract void SetRestartTime();
    void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.name == colliderName)
        {
            SetRestartTime();
            Fear.objInstance.DieAfterJump();
            dieAfterJumpSound.Play();
            isTriggered = true;
            StartCoroutine(Fear.objInstance.FadeIn(restartTime));
        }
    }
}