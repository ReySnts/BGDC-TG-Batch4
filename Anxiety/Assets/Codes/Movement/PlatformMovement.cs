using UnityEngine;
public class PlatformMovement : MonoBehaviour
{
    Animator platformAnimControl = null;
    string objName = null;
    int len = 0;
    void Start()
    {
        platformAnimControl = GetComponent<Animator>();
        objName = gameObject.name;
        len = objName.Length;
        platformAnimControl.Play("Platform" + objName[len - 1] + "Anim");
    }
}