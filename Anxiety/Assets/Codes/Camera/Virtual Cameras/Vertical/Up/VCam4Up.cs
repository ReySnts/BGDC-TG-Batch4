using UnityEngine;
public class VCam4Up : VCamVerticalUp
{
    protected override Vector2 SetVCam()
    {
<<<<<<< HEAD
        return vCamVerticalDown.yTrigger = 28.8f;
=======
        yTriggerX = 26.5f;
        yTriggerY = 40f;
        return new Vector2(yTriggerX, yTriggerY);
>>>>>>> programming
    }
}