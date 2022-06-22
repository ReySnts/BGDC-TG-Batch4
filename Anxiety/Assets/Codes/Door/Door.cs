using System.Collections;
using UnityEngine;
public class Door : MonoBehaviour
{
    string colliderName = "Player";
    public Animator doorControl = null;
    [Header("Checks")]
    [SerializeField] bool isTriggered = false;
    [SerializeField] bool onClick = false;
    [SerializeField] bool isOpened = true;
    void OnTriggerEnter(Collider other)
    {
        if (other.name == colliderName) isTriggered = true;
    }
    void OnTriggerExit(Collider other)
    {
        if (other.name == colliderName) isTriggered = false;
    }
    void Start()
    {
        isOpened = true;
    }
    IEnumerator HoldDoor()
    {
        onClick = true;
        yield return new WaitForSeconds(1f);
        onClick = false;
    }
    void Update()
    {
        if (isTriggered && !onClick && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(HoldDoor());
            doorControl.SetBool("IsOpened", isOpened);
            if (isOpened) isOpened = false;
            else isOpened = true;
        }
    }
}