using UnityEngine;
public class NextScene : MonoBehaviour
{
    string colliderName = "Player";
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == colliderName)
        {
            SceneManagement.NextScene();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.name == colliderName)
        {
            SceneManagement.NextScene();
        }
    }
}