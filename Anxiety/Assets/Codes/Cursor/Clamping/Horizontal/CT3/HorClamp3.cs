using UnityEngine;
public class HorClamp3 : MonoBehaviour
{
    [Header("References")]
    public Cursor3 leftCursor = null;
    public Cursor4 rightCursor = null;
    [Header("Values")]
    [SerializeField] bool isTurn = false;
    [SerializeField] bool LeftToRight = false;
    string colliderName = "Player";
    void Start()
    {
        leftCursor = FindObjectOfType<Cursor3>();
        rightCursor = FindObjectOfType<Cursor4>();
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