using UnityEngine;
public class VCam4Up : VCamVerticalUp
{
    public override float SetVCam()
    {
        return vCamVerticalDown.yTrigger = 28.8f;
    }
}