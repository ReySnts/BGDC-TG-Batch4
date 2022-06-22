public class HorClamp6 : Clamp
{
    protected override void SetCursor()
    {
        leftCursor = FindObjectOfType<Cursor6>();
        rightCursor = FindObjectOfType<Cursor7>();
    }
}