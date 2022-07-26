using UnityEngine;
using System.Collections;
public class HandWall : MonoBehaviour
{
    public Animator boneAnimControl = null;
    Fear fear = null;
    string colliderName = "Player";
    bool isPlayerFelt = false;
    bool isAttHeld = false;
    float waitTime = 1f;
    float handDmg = 34f;
    void Start()
    {
        fear = FindObjectOfType<Fear>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.name == colliderName) isPlayerFelt = true;
    }
    void OnTriggerExit(Collider other)
    {
        if (other.name == colliderName) isPlayerFelt = false;
    }
    void Attack()
    {
        fear.fearMeter.value += handDmg;
        isAttHeld = false;
    }
    IEnumerator HoldAttack()
    {
        isAttHeld = true;
        yield return new WaitForSeconds(waitTime);
        Attack();
    }
    void Update()
    {
        if (!isAttHeld && isPlayerFelt) StartCoroutine(HoldAttack());
        boneAnimControl.SetBool("IsPlayerFelt", isPlayerFelt);
    }
}