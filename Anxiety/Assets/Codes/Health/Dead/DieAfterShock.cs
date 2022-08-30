using UnityEngine;
public class DieAfterShock : Death
{
    public AudioSource dieAfterShockSound = null;
    void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.name == colliderName)
        {
            isTriggered = true;
            restartTime = 1.5f;
            Fear.objInstance.DieAfterShockBecauseSpike();
            dieAfterShockSound.Play();
            StartCoroutine(Fear.objInstance.FadeIn(restartTime));
        }
    }
}