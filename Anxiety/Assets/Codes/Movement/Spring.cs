using UnityEngine;
public class Spring : MonoBehaviour
{
    [Header("References")]
    public Rigidbody playerBody = null;
    PlayerMovement playerMovement = null;
    [Header("Values")]
    [SerializeField] bool triggered = false;
    string colliderName = "Player";
    void Update()
    {
        if (playerMovement == null) playerMovement = FindObjectOfType<PlayerMovement>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.name == colliderName)
        {
            playerMovement.jumpForce += 2.5f;
            triggered = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (triggered && other.name == colliderName)
        {
            playerMovement.jumpForce -= 2.5f;
            triggered = false;
        }
    }
}