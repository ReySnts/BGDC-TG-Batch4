public class HorClamp4 : HorClamp
{
    protected override void SetCursor()
    {
        leftCursor = FindObjectOfType<Cursor4>();
        rightCursor = FindObjectOfType<Cursor5>();
    }
}