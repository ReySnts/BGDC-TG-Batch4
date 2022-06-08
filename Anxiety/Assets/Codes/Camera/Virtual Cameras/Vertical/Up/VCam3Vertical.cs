using UnityEngine;
public class VCam3Vertical : VCamVerticalUp
{
    float y = 0f;
    public override void SetVCam(float y)
    {
        yTrigger = y;
    }
    void Awake()
    {
        y = vCamVerticalDown.yTrigger = 15.2f;
        SetVCam(y);
    }
}