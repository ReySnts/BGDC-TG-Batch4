using UnityEngine;
public class VCam3Vertical : VCamVerticalUp
{
    float y = 0f;
    public override void SetVCam(float y)
    {
        yTrigger = y;
    }
    public override void SetCursor()
    {
        yClamp.x = 0f;
        yClamp.y = 12f;
    }
    void Awake()
    {
        y = vCamVerticalDown.yTrigger = 14f;
        SetVCam(y);
        SetCursor();
    }
}