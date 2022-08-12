using UnityEngine;
public class Spring : MonoBehaviour
{
    public Rigidbody playerBody = null;
    [SerializeField] bool triggered = false;
    string colliderName = "Player";
    void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.name == colliderName)
        {
            PlayerMovement.objInstance.jumpForce += 2.5f;
            triggered = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (triggered && other.name == colliderName)
        {
            PlayerMovement.objInstance.jumpForce -= 2.5f;
            triggered = false;
        }
    }
}