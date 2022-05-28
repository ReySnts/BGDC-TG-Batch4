using UnityEngine;
using UnityEngine.Playables;
public class VCam1 : VCams
{
    float x = 0f;
    public override void SetVCam(float xTriggerX)
    {
        xTrigger = new Vector2(xTriggerX, -143f);
        yClamp = new Vector2(3.4f, 13.45f);
    }
    void Awake()
    {
        x = cam2D.firstXTrigger.x;
        SetVCam(x);
    }
}