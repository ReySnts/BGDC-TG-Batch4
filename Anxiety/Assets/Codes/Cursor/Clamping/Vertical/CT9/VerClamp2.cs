using UnityEngine;
public class VerClamp2 : MonoBehaviour
{
    [Header("References")]
    public Cursor9 leftCursor = null;
    public Cursor10 rightCursor = null;
    [Header("Values")]
    [SerializeField] bool isTurn = false;
    [SerializeField] bool LeftToRight = false;
    string colliderName = "Player";
    void Start()
    {
        leftCursor = FindObjectOfType<Cursor9>();
        rightCursor = FindObjectOfType<Cursor10>();
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