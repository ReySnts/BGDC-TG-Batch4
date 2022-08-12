using UnityEngine;
public abstract class DieAfterJump : Death
{
    public AudioSource dieAfterJumpSound = null;
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