public class HorClamp9 : Clamp
{
    protected override void SetCursor()
    {
        leftCursor = FindObjectOfType<Cursor11>();
        rightCursor = FindObjectOfType<Cursor12>();
    }
}