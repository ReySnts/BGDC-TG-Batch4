using UnityEngine;
public class VCam6 : VCamHorizontal
{
    float x = 0f;
    [Header("Special")]
    public Cam2DToVCam6 cam2DToVCam6 = null;
    public override void SetVCam(float xTriggerX)
    {
        xTrigger = new Vector2(xTriggerX, -63f);
    }
    void Awake()
    {
        cam2D = cam2DToVCam6;
        x = cam2D.xTriggers[0].x;
        SetVCam(x);
    }
}