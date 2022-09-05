using UnityEngine;
public abstract class DieAfterJump : Death
{
    public AudioSource dieAfterJumpSound = null;
    protected abstract void SetRestartTime();
    void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.name == colliderName)
        {
            isTriggered = true;
            SetRestartTime();
            Fear.objInstance.DieAfterJump();
            try
            {
                dieAfterJumpSound.Play();
            }
            catch { }
            StartCoroutine(Fear.objInstance.FadeIn(restartTime));
        }
    }
}