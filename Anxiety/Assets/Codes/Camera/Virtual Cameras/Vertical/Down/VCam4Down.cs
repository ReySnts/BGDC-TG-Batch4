using UnityEngine;
public class VCam4Down : VCamVerticalDown
{
    protected override Vector2 SetVCam()
    {
        yTriggerX = 0f;
        yTriggerY = 14.15f;
        return new Vector2(yTriggerX, yTriggerY);
    }
}