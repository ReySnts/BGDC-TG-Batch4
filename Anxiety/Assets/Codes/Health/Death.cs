using UnityEngine;
public class Death : MonoBehaviour
{
    string colliderName = "Player";
    void OnTriggerEnter(Collider other)
    {
        if (other.name == colliderName)
        {
            SceneManagement.Restart();
        }
    }
}