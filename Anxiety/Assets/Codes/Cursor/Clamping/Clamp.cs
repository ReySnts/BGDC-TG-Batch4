using UnityEngine;
public class Clamp : MonoBehaviour
{
    protected string colliderName = "Player";
    [Header("References")]
    public SetCursor leftCursor = null;
    public SetCursor rightCursor = null;
    [Header("Values")]
    [SerializeField] protected bool clampCheckDie = false;
    [SerializeField] protected bool toRight = false;
    protected bool isTurn = false;
    protected virtual void SetCursor() { }
    void Start()
    {
        SetCursor();
    }
    void OnTriggerEnter(Collider other)
    {
        if (isTurn && other.name == colliderName) isTurn = false;
    }
    protected void CheckTurn()
    {
        if (toRight)
        {
            rightCursor.SetClamp();
            rightCursor.SetLight();
        }
        else if (!toRight || clampCheckDie)
        {
            leftCursor.SetClamp();
            leftCursor.SetLight();
            clampCheckDie = false;
        }
    }
    protected void TurnOnLeftCursor()
    {
        clampCheckDie = true;
        Fear.hasDied = toRight = false;
        CheckTurn();
    }
}