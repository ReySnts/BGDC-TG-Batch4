public class Cursor8 : SetCursor
{
    public override void SetClamp()
    {
        lightCursor.leftXClamp = -110f;
        lightCursor.rightXClamp = -86f;
        lightCursor.downYClamp = 1f;
        lightCursor.upYClamp = 12f;
    }
    public override void SetLight()
    {
        spotLight.range = 3.2f;
        spotLight.intensity = 1.5f;
    }
}