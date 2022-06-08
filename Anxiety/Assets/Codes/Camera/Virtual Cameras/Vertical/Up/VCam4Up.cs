using UnityEngine;
public class VCam4Up : VCamVerticalUp
{
    float y = 0f;
    public override void SetVCam(float y)
    {
        yTrigger = y;
    }
    void Awake()
    {
        y = vCamVerticalDown.yTrigger = 28.8f;
        SetVCam(y);
    }
}