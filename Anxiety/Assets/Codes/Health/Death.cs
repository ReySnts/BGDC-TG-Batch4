using UnityEngine;
public class Death : MonoBehaviour
{
    string colliderName = "Player";
    bool isTriggered = false;
    void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.name == colliderName)
        {
            FindObjectOfType<Fear>().Die();
            isTriggered = true;
        }
    }
}