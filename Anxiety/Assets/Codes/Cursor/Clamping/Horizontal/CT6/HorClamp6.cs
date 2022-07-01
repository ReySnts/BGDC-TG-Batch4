public class HorClamp6 : HorClamp
{
    protected override void SetCursor()
    {
        leftCursor = FindObjectOfType<Cursor6>();
        rightCursor = FindObjectOfType<Cursor7>();
    }
}