using UnityEngine;
public class VCam6 : VCamHorizontal
{
    public override void SetVCam()
    {
        cam2D = FindObjectOfType<Cam2DToVCam6>();
        xTrigger = new Vector2(cam2D.xTriggers[0].x, -63f);
    }
}