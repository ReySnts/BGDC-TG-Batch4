using UnityEngine;
using UnityEngine.Playables;
public class VCam3 : VCams
{
    float x = 0f;
    public override void SetVCam(float xTriggerX)
    {
        xTrigger = new Vector2(xTriggerX, -82f);
        yClamp = new Vector2(0f, 12f);
    }
    void Awake()
    {
        x = cam2D.thirdXTrigger.x;
        SetVCam(x);
    }
}