using UnityEngine;
public class Death : MonoBehaviour
{
    public bool isCollided = false;
    public bool isTriggered = false;
    protected string colliderName = "Player";
    protected float restartTime = 0f;
}