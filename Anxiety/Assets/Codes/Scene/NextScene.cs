using UnityEngine;
public class NextScene : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            SceneManagement.NextScene();
        }
    }
}