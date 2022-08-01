using UnityEngine;
using System.Collections;
public class HandWall : MonoBehaviour
{
    [Header("References")]
    public Animator boneAnimControl = null;
    public Transform player = null;
    public Transform handMagnet = null;
    Fear fear = null;
    PlayerMovement playerMovement = null;
    [Header("Values")]
    string colliderName = "Player";
    bool isPlayerFelt = false;
    bool isAttHeld = false;
    public static bool turnOnMagnet = false;
    public static bool hasGrabbedPlayer = false;
    float waitTime = 1f;
    float handDmg = 35f;
    float fearValForGrab = 60f;
    float zDiff = -0.18f;
    float grabSpeed = 0.0125f;
    Vector3 handMagnetPosition = Vector3.zero;
    void Start()
    {
        fear = FindObjectOfType<Fear>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        handMagnetPosition = handMagnet.position + Vector3.forward * zDiff;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.name == colliderName) isPlayerFelt = true;
    }
    void OnTriggerExit(Collider other)
    {
        if (other.name == colliderName) isPlayerFelt = false;
    }
    void SetAnim()
    {
        boneAnimControl.SetBool("IsPlayerFelt", isPlayerFelt);
        boneAnimControl.SetFloat("FearValue", fear.fearMeter.value);
        boneAnimControl.SetBool("HasDoneQTE", Fear.hasDoneQTE);
    }
    void Grab()
    {
        if (player.position == handMagnetPosition) hasGrabbedPlayer = true;
        else player.position = Vector3.MoveTowards(player.position, handMagnetPosition, grabSpeed);
    }
    IEnumerator HoldAttack()
    {
        isAttHeld = true;
        yield return new WaitForSeconds(waitTime);
        if (fear.fearMeter.value < fearValForGrab) fear.fearMeter.value += handDmg;
        isAttHeld = false;
    }
    void Attack()
    {
        if (!isAttHeld) StartCoroutine(HoldAttack());
        if (fear.fearMeter.value >= fearValForGrab)
        {
            playerMovement.enabled = playerMovement.rigidBody.useGravity = false;
            playerMovement.rigidBody.isKinematic = isAttHeld = turnOnMagnet = true;
        }
    }
    void Update()
    {
        if (isPlayerFelt)
        {
            if (!turnOnMagnet) Attack();
            else if (!hasGrabbedPlayer) Grab();
        }
        SetAnim();
    }
}