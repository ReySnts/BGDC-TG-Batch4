public class Cursor3 : SetCursor
{
    public override void SetClamp()
    {
        lightCursor.leftXClamp = -144f;
        lightCursor.rightXClamp = -139f;
        lightCursor.downYClamp = 9f;
        lightCursor.upYClamp = 9.15f;
    }
    public override void SetLight()
    {
        spotLight.range = 3.2f;
        spotLight.intensity = 1.5f;
    }
}