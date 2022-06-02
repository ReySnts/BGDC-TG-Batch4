using UnityEngine;
public class VCam3Horizontal : VCamHorizontal
{
    float x = 0f;
    public override void SetVCam(float xTriggerX)
    {
        xTrigger = new Vector2(xTriggerX, -44f);
        yClamp = new Vector2(0f, 12f);
    }
    void Awake()
    {
        x = cam2D.xTriggers[2].x;
        SetVCam(x);
    }
}