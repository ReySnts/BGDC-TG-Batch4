public class HorClamp1 : Clamp
{
    protected override void SetCursor()
    {
        leftCursor = FindObjectOfType<Cursor1>();
        rightCursor = FindObjectOfType<Cursor2>();
    }
}