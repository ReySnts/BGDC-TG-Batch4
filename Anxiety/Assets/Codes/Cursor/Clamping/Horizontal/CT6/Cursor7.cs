public class Cursor7 : SetCursor
{
    public override void SetClamp()
    {
        lightCursor.leftXClamp = -116f;
        lightCursor.rightXClamp = -113f;
        lightCursor.downYClamp = 11.3f;
        lightCursor.upYClamp = 13f;
    }
    public override void SetLight()
    {
        spotLight.range = 3.2f;
        spotLight.intensity = 1.5f;
        sphere.localScale = UnityEngine.Vector3.right * spotLight.range * 2f;
    }
}