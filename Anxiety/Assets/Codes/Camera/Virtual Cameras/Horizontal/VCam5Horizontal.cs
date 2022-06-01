using UnityEngine;
public class VCam5Horizontal : VCamHorizontal
{
    float x = 0f;
    public override void SetVCam(float xTriggerX)
    {
        xTrigger = new Vector2(xTriggerX, -82f);
        yClamp = FindObjectOfType<VCam4Up>().yClamp;
    }
    void Awake()
    {
        x = cam2D.xTriggers[3].x;
        SetVCam(x);
    }
}