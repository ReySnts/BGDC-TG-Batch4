using UnityEngine;
public class VCam1 : VCamHorizontal
{
    float x = 0f;
    public override void SetVCam(float xTriggerX)
    {
        xTrigger = new Vector2(xTriggerX, -143f);
    }
    void Awake()
    {
        x = cam2D.xTriggers[0].x;
        SetVCam(x);
    }
}