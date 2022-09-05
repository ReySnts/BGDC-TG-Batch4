using UnityEngine;
public class VCam3Vertical : VCamVerticalUp
{
    protected override Vector2 SetVCam()
    {
<<<<<<< HEAD
        return vCamVerticalDown.yTrigger = 15.2f;
=======
        yTriggerX = 14.15f;
        yTriggerY = 26.5f;
        return new Vector2(yTriggerX, yTriggerY);
>>>>>>> programming
    }
}