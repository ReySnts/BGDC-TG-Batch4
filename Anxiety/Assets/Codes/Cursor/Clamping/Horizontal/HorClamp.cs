using UnityEngine;
public class HorClamp : Clamp
{
    void OnTriggerExit(Collider other)
    {
        if (!isTurn && other.name == colliderName)
        {
            if (other.transform.position.x < transform.position.x) toRight = false;
            else if (other.transform.position.x > transform.position.x) toRight = true;
            CheckTurn();
            isTurn = true;
        }
    }
}