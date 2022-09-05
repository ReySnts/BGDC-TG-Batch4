using UnityEngine;
public class Spring : MonoBehaviour
{
    [Header("References")]
    public Rigidbody playerBody = null;
    public PlayerMovement playerMovement = null;
    [Header("Values")]
    [SerializeField] float initForce = 7.5f;
    [SerializeField] bool triggered = false;
<<<<<<< HEAD
=======
    readonly string colliderName = "Player";
>>>>>>> programming
    void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.name == "Player")
        {
            playerMovement.jumpForce = 2 * initForce;
            triggered = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        playerMovement.jumpForce = initForce;
        triggered = false;
    }
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }
}