using UnityEngine;
public class VCam4Up : VCamVerticalUp
{
    float y = 0f;
    public override void SetVCam(float y)
    {
        yTrigger = y;
    }
    public override void SetCursor()
    {
        yClamp.x = 16f;
        yClamp.y = 25f;
    }
    void Awake()
    {
        y = vCamVerticalDown.yTrigger = 27f;
        SetVCam(y);
        SetCursor();
    }
}