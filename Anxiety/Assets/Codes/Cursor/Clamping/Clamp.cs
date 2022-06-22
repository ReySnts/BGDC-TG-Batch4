using UnityEngine;
public abstract class Clamp : MonoBehaviour
{
    [Header("References")]
    public SetCursor leftCursor = null;
    public SetCursor rightCursor = null;
    [Header("Values")]
    public bool isTurn = false;
    public bool LeftToRight = false;
    protected string colliderName = "Player";
    protected abstract void SetCursor();
    void Start()
    {
        SetCursor();
    }
    void OnTriggerEnter(Collider other)
    {
        if (!isTurn && other.name == colliderName)
        {
            if (!LeftToRight)
            {
                rightCursor.SetClamp();
                rightCursor.SetLight();
                LeftToRight = true;
            }
            else
            {
                leftCursor.SetClamp();
                leftCursor.SetLight();
                LeftToRight = false;
            }
            isTurn = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (isTurn && other.name == colliderName) isTurn = false;
    }
}