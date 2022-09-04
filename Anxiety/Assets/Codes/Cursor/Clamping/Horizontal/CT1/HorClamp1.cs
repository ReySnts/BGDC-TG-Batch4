public class HorClamp1 : HorClamp
{
    protected override void SetCursor()
    {
        leftCursor = FindObjectOfType<Cursor1>();
        rightCursor = FindObjectOfType<Cursor2>();
    }
    void Update()
    {
        if (
            Fear.objInstance.hasDied &&
            (
                CheckPoint.objInstance.checkPointName.Length == 0 ||
                CheckPoint.objInstance.checkPointName == CheckPoint.objInstance.colliderName
            )
        ) TurnOnLeftCursor();
    }
}