using UnityEngine;
using System;
using System.Collections;
public class HandWall : MonoBehaviour
{
    [Header("References")]
    public Animator boneAnimControl = null;
    public Transform player = null;
    string colliderName = "Player";
    PlayerMovement playerMovement = null;
    public Transform handMagnet = null;
    Vector3 handMagnetPosition = Vector3.zero;
    public static Action<bool> stopMovement = null;
    [Header("Booleans")]
    public static bool turnOnMagnet = false;
    public static bool hasGrabbedPlayer = false;
    public static bool startPulling = false;
    public static bool hasPulled = false;
    bool isPlayerFelt = false;
    bool isAttHeld = false;
    [Header("Floats")]
    static float lastGrabbedTime = 0f;
    float grabDuration = 0.01f;
    float waitTime = 1f;
    float handDmg = 35f;
    float fearValForGrab = 60f;
    float zDiff = -0.18f;
    float grabSpeed = 0.0125f;
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.name == colliderName)
        {
            if (Time.time - lastGrabbedTime < grabDuration) isPlayerFelt = false;
            else
            {
                if (turnOnMagnet) lastGrabbedTime = Time.time;
                isPlayerFelt = true;
                stopMovement?.Invoke(isPlayerFelt);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.name == colliderName) isPlayerFelt = false;
    }
    void SetAnim()
    {
        boneAnimControl.SetBool("IsPlayerFelt", isPlayerFelt);
        boneAnimControl.SetFloat("FearValue", Fear.objInstance.fearMeter.value);
        boneAnimControl.SetBool("HasDoneQTE", Fear.objInstance.hasDoneQTE);
        boneAnimControl.SetBool("HasPulled", hasPulled);
    }
    IEnumerator HoldRestart()
    {
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(Fear.objInstance.FadeIn(waitTime));
        StartCoroutine(Fear.objInstance.ResetHand(5f));
    }
    void Pull()
    {
        if (!startPulling)
        {
            StartCoroutine(HoldRestart());
            startPulling = true;
        }
        //handMagnetPosition = handMagnet.position + Vector3.forward * zDiff;
        player.position = handMagnetPosition;
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
        if (Fear.objInstance.fearMeter.value < fearValForGrab) Fear.objInstance.fearMeter.value += handDmg;
        isAttHeld = false;
    }
    void Attack()
    {
        if (Fear.objInstance.fearMeter.value >= fearValForGrab)
        {
            playerMovement.enabled = playerMovement.rigidBody.useGravity = false;
            playerMovement.rigidBody.isKinematic = turnOnMagnet = true;
        }
        else if (!isAttHeld) StartCoroutine(HoldAttack());
    }
    void Update()
    {
        handMagnetPosition = handMagnet.position + Vector3.forward * zDiff;
        if (isPlayerFelt)
        {
            if (!turnOnMagnet) Attack();
            else if (!hasGrabbedPlayer) Grab();
            else if (Fear.objInstance.isDieAfterShock) Pull();
        }
        SetAnim();
    }
}