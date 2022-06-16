public class Cursor5 : SetCursor
{
    public override void SetClamp()
    {
        lightCursor.leftXClamp = -136f;
        lightCursor.rightXClamp = -131f;
        lightCursor.downYClamp = 9f;
        lightCursor.upYClamp = 11f;
    }
    public override void SetLight()
    {
        spotLight.range = 3.2f;
        spotLight.intensity = 1.5f;
        sphere.localScale = UnityEngine.Vector3.right * spotLight.range * 2f;
    }
}