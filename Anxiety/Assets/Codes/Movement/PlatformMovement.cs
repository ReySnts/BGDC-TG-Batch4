using UnityEngine;
public class PlatformMovement : MonoBehaviour
{
    Animator platformAnimControl = null;
    void Start()
    {
        platformAnimControl = GetComponent<Animator>();
        platformAnimControl.Play(gameObject.name);
    }
}