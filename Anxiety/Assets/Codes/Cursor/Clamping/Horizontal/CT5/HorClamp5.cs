public class HorClamp5 : HorClamp
{
    protected override void SetCursor()
    {
        leftCursor = FindObjectOfType<Cursor5>();
        rightCursor = FindObjectOfType<Cursor6>();
    }
}