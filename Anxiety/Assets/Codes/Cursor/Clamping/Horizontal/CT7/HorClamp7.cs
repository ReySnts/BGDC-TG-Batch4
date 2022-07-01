public class HorClamp7 : HorClamp
{
    protected override void SetCursor()
    {
        leftCursor = FindObjectOfType<Cursor7>();
        rightCursor = FindObjectOfType<Cursor8>();
    }
}