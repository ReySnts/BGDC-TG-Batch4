public class Cursor12 : SetCursor
{
    public override void SetClamp()
    {
        lightCursor.leftXClamp = -68f;
        lightCursor.rightXClamp = -65f;
        lightCursor.downYClamp = 5f;
        lightCursor.upYClamp = 36f;
    }
    public override void SetLight()
    {
        spotLight.range = 4.5f;
        spotLight.intensity = 2.25f;
        sphere.localScale = UnityEngine.Vector3.right * spotLight.range * 2f;
    }
}