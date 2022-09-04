using UnityEngine;
public class DieAfterShock : Death
{
    public AudioSource dieAfterShockSound = null;
    [Header("Y Positions")]
    protected float platformYPosition = 0f;
    protected float playerYPosition = 0f;
    readonly float yPositionDifference = 6.5f;
    protected virtual void OnCollisionEnter(Collision collision)
    {
        platformYPosition = transform.position.y;
        playerYPosition = collision.gameObject.transform.position.y;
        if (
            !isCollided &&
            collision.gameObject.name == colliderName &&
            platformYPosition - playerYPosition >= yPositionDifference
        ) isCollided = true;
    }
    protected void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.name == colliderName)
        {
            isTriggered = true;
            restartTime = 1.5f;
            Fear.objInstance.DieAfterShockBecauseSpike();
            try
            {
                dieAfterShockSound.Play();
            }
            catch { }
            StartCoroutine(Fear.objInstance.FadeIn(restartTime));
        }
    }
    protected void Update()
    {
        try
        {
            GetComponent<BoxCollider>().isTrigger = isCollided;
        }
        catch { }
    }
}