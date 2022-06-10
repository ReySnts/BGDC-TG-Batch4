public class Cursor9 : SetCursor
{
    public override void SetClamp()
    {
        lightCursor.leftXClamp = -110f;
        lightCursor.rightXClamp = -86f;
        lightCursor.downYClamp = 16f;
        lightCursor.upYClamp = 24.5f;
    }
    public override void SetLight()
    {
        spotLight.range = 4.8f;
        spotLight.intensity = 2.4f;
    }
}