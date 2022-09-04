public class HitByCube : DieAfterShock
{
    protected override void OnCollisionEnter(UnityEngine.Collision collision)
    {
        platformYPosition = transform.position.y;
        playerYPosition = collision.gameObject.transform.position.y;
        if (
            !isCollided && 
            collision.gameObject.name == colliderName && 
            platformYPosition > playerYPosition
        ) isCollided = true;
    }
}