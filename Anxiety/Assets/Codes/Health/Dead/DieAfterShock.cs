using UnityEngine;
public class DieAfterShock : Death
{
    public AudioSource dieAfterShockSound = null;
    void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.name == colliderName)
        {
            restartTime = 1.5f;
            Fear.objInstance.DieAfterShockBecauseSpike();
            dieAfterShockSound.Play();
            isTriggered = true;
            StartCoroutine(Fear.objInstance.FadeIn(restartTime));
        }
    }
}