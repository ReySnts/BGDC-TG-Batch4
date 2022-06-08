using UnityEngine;
public class HorClamp5 : MonoBehaviour
{
    [Header("References")]
    public Cursor5 leftCursor = null;
    public Cursor6 rightCursor = null;
    [Header("Values")]
    [SerializeField] bool isTurn = false;
    [SerializeField] bool LeftToRight = false;
    string colliderName = "Player";
    void Start()
    {
        leftCursor = FindObjectOfType<Cursor5>();
        rightCursor = FindObjectOfType<Cursor6>();
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