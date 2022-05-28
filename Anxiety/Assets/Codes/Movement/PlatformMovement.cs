using UnityEngine;
public class PlatformMovement : MonoBehaviour
{
    public Transform eyePos = null;
    float zOffset = 0f;
    Animator platformAnimControl = null;
    string objName = null;
    int len = 0;
    void Start()
    {
        platformAnimControl = GetComponent<Animator>();
        objName = gameObject.name;
        len = objName.Length;
        platformAnimControl.Play("Platform" + objName[len - 1] + "Anim");
        zOffset = transform.position.z - eyePos.position.z;
    }
    void Update()
    {
        eyePos.position = transform.position + Vector3.forward * zOffset;
    }
}