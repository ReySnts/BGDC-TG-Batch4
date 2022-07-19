public class HorClamp1 : HorClamp
{
    protected override void SetCursor()
    {
        leftCursor = FindObjectOfType<Cursor1>();
        rightCursor = FindObjectOfType<Cursor2>();
    }
    void Update()
    {
        if (Fear.hasDied && CheckPoint.checkPointName == "Moshrum") TurnOnLeftCursor();
    }
}