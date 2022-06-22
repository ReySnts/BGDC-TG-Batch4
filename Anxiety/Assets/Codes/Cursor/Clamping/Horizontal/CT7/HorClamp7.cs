public class HorClamp7 : Clamp
{
    protected override void SetCursor()
    {
        leftCursor = FindObjectOfType<Cursor7>();
        rightCursor = FindObjectOfType<Cursor8>();
    }
}