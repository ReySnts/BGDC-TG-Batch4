using UnityEngine;
public class VCam2 : VCamHorizontal
{
    protected override void SetVCam()
    {
        xTrigger = new Vector2(cam2D.xTriggers[1].x, -116f);
    }
}