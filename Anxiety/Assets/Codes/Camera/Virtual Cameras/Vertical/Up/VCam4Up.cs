using UnityEngine;
public class VCam4Up : VCamVerticalUp
{
    protected override Vector2 SetVCam()
    {
        yTriggerX = 26.5f;
        yTriggerY = 40f;
        return new Vector2(yTriggerX, yTriggerY);
    }
}