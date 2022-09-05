using UnityEngine;
using System.Collections;
public class HandWall : MonoBehaviour
{
    [Header("References")]
    public Animator boneAnimControl = null;
    public Transform player = null;
    public Transform handMagnet = null;
    readonly string colliderName = "Player";
    [Header("Booleans")]
    public static bool turnOnMagnet = false;
    public static bool hasGrabbedPlayer = false;
    public static bool startPulling = false;
    public static bool hasPulled = false;
    bool isPlayerFelt = false;
    bool isAttHeld = false;
    [Header("Floats")]
    public static float lastGrabbedTime = 0f;
    readonly float grabDuration = 0.01f;
    readonly float waitTime = 1f;
    readonly float handDmg = 35f;
    readonly float fearValForGrab = 60f;
    readonly float zDiff = -0.18f;
    readonly float grabSpeed = 0.0125f;
    Vector3 handMagnetPosition = Vector3.zero;
    void OnTriggerEnter(Collider other)
    {
        if (other.name == colliderName)
        {
            if (Time.time - lastGrabbedTime < grabDuration) isPlayerFelt = false;
            else
            {
                if (turnOnMagnet) lastGrabbedTime = Time.time;
                isPlayerFelt = true;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.name == colliderName) isPlayerFelt = false;
    }
    IEnumerator HoldRestart()
    {
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(Fear.objInstance.FadeIn(waitTime));
        StartCoroutine(Fear.objInstance.ResetHand(5f));
    }
    IEnumerator HoldAttack()
    {
        isAttHeld = true;
        yield return new WaitForSeconds(waitTime);
        if (Fear.objInstance.fearMeter.value < fearValForGrab) Fear.objInstance.fearMeter.value += handDmg;
        isAttHeld = false;
    }
    void Update()
    {
        handMagnetPosition = handMagnet.position + Vector3.forward * zDiff;
        if (!Fear.objInstance.isDie && isPlayerFelt)
        {
            if (!turnOnMagnet)
            {
                #region Attack Player
                if (Fear.objInstance.fearMeter.value >= fearValForGrab)
                {
                    PlayerMovement.objInstance.enabled = PlayerMovement.objInstance.rigidBody.useGravity = false;
                    PlayerMovement.objInstance.rigidBody.isKinematic = turnOnMagnet = true;
                    Fear.objInstance.playerMoveSounds.SetActive(false);
                }
                else if (!isAttHeld) StartCoroutine(HoldAttack());
                #endregion
            }
            else if (!hasGrabbedPlayer)
            {
                #region Grab Player
                if (player.position == handMagnetPosition) hasGrabbedPlayer = true;
                else player.position = Vector3.MoveTowards(player.position, handMagnetPosition, grabSpeed);
                #endregion
            }
            else if (Fear.objInstance.isDieAfterShock)
            {
                #region Pull Player
                if (!startPulling)
                {
                    StartCoroutine(HoldRestart());
                    startPulling = true;
                }
                player.position = handMagnetPosition;
                #endregion
            }
        }
        #region Set HandWalls Animations
        boneAnimControl.SetBool("IsPlayerFelt", isPlayerFelt);
        boneAnimControl.SetFloat("FearValue", Fear.objInstance.fearMeter.value);
        boneAnimControl.SetBool("HasDoneQTE", Fear.objInstance.hasDoneQTE);
        boneAnimControl.SetBool("HasPulled", hasPulled);
        #endregion
    }
}