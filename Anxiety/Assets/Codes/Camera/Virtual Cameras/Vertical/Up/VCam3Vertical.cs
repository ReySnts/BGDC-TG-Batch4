using UnityEngine;
public class VCam3Vertical : VCamVerticalUp
{
    public override float SetVCam()
    {
        return vCamVerticalDown.yTrigger = 14.15f;
    }
}