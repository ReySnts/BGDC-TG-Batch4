using System.Collections;
using UnityEngine;
public class Door : MonoBehaviour
{
    string colliderName = "Player";
    float waitTime = 1f;
    [Header("References")]
    public Animator doorControl = null;
    public BoxCollider doorCollider = null;
    public GameObject player = null;
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
    IEnumerator StartScene()
    {
        doorCollider.enabled = false;
        player.SetActive(false);
        yield return new WaitForSeconds(waitTime);
        doorCollider.enabled = true;
        player.SetActive(true);
    }
    void Start()
    {
        StartCoroutine(StartScene());
        isOpened = true;
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
            doorControl.SetBool("IsOpened", isOpened);
            if (isOpened) isOpened = false;
            else isOpened = true;
        }
    }
}