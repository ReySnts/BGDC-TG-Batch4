using UnityEngine;
public class VerClamp : Clamp
{
    void OnTriggerExit(Collider other)
    {
        if (!isTurn && other.name == colliderName)
        {
            if (other.transform.position.y < transform.position.y) toRight = false;
            else if (other.transform.position.y > transform.position.y) toRight = true;
            CheckTurn();
            isTurn = true;
        }
    }
}