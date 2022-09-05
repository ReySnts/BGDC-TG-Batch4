using UnityEngine;
public class VCam5Vertical : VCamVerticalDown
{
    protected override Vector2 SetVCam()
    {
        yTriggerX = 14.15f;
        yTriggerY = 26.5f;
        return new Vector2(yTriggerX, yTriggerY);
    }
}