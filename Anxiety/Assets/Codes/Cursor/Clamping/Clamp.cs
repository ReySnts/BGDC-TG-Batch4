using UnityEngine;
public class Clamp : MonoBehaviour
{
    protected string colliderName = "Player";
    [Header("References")]
    public SetCursor leftCursor = null;
    public SetCursor rightCursor = null;
    [Header("Values")]
    protected bool isTurn = false;
    protected bool toRight = false;
    protected virtual void SetCursor() { }
    void Start()
    {
        SetCursor();
    }
    public void CheckTurn()
    {
        if (toRight)
        {
            rightCursor.SetClamp();
            rightCursor.SetLight();
        }
        else
        {
            leftCursor.SetClamp();
            leftCursor.SetLight();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (isTurn && other.name == colliderName) isTurn = false;
    }
}