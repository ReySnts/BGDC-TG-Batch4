using System.Collections;
using UnityEngine;
public class Door : MonoBehaviour
{
    protected string colliderName = "Player";
    protected string animParamName = "IsOpened";
    protected float waitTime = 1f;
    [Header("References")]
    public Animator doorControl = null;
    public BoxCollider doorCollider = null;
    public GameObject player = null;
    [Header("Checks")]
    [SerializeField] protected bool isTriggered = false;
    [SerializeField] protected bool onClick = false;
    [SerializeField] protected bool isOpened = true;
    void OnTriggerEnter(Collider other)
    {
        if (other.name == colliderName) isTriggered = true;
    }
    void OnTriggerExit(Collider other)
    {
        if (other.name == colliderName) isTriggered = false;
    }
    IEnumerator HoldDoor()
    {
        onClick = true;
        yield return new WaitForSeconds(waitTime);
        onClick = false;
    }
    void Update()
    {
        if (isTriggered && !onClick && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(HoldDoor());
            if (isOpened) isOpened = false;
            else isOpened = true;
            doorControl.SetBool(animParamName, isOpened);
        }
    }
}