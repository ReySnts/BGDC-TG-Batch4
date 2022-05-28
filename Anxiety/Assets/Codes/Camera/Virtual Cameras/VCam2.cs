using UnityEngine;
using UnityEngine.Playables;
public class VCam2 : VCams
{
    float x = 0f;
    public override void SetVCam(float xTriggerX)
    {
        xTrigger = new Vector2(xTriggerX, -116f);
        yClamp = new Vector2(3.4f, 13.45f);
    }
    void Awake()
    {
        x = cam2D.secondXTrigger.x;
        SetVCam(x);
    }
}