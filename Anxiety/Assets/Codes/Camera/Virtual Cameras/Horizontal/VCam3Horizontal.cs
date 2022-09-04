using UnityEngine;
public class VCam3Horizontal : VCamHorizontal
{
    protected override void SetVCam()
    {
        xTrigger = new Vector2(cam2D.xTriggers[2].x, -44f);
    }
}