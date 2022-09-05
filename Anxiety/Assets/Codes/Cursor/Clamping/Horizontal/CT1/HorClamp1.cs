public class HorClamp1 : Clamp
{
    protected override void SetCursor()
    {
        leftCursor = FindObjectOfType<Cursor1>();
        rightCursor = FindObjectOfType<Cursor2>();
    }
<<<<<<< HEAD
=======
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
>>>>>>> programming
}