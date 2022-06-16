using UnityEngine;
public class GetLight : MonoBehaviour
{
    public static bool outOfLight = false;
    string colliderName = "Player";
    void OnTriggerEnter(Collider other)
    {
        if (other.name == colliderName) outOfLight = false;
    }
    void OnTriggerExit(Collider other)
    {
        if (other.name == colliderName) outOfLight = true;
    }
}