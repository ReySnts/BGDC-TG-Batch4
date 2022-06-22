public class HorClamp5 : Clamp
{
    protected override void SetCursor()
    {
        leftCursor = FindObjectOfType<Cursor5>();
        rightCursor = FindObjectOfType<Cursor6>();
    }
}