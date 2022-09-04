using UnityEngine;
public class VCam1 : VCamHorizontal
{
    protected override void SetVCam()
    {
        xTrigger = new Vector2(cam2D.xTriggers[0].x, -143f);
    }
}