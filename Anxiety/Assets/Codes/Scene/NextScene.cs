using UnityEngine;
public class NextScene : MonoBehaviour
{
    public SceneManagement sceneManagement = null;   
    void Start()
    {
        sceneManagement = FindObjectOfType<SceneManagement>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            sceneManagement.NextScene();
        }
    }
}