public class VerClamp1 : Clamp
{
    protected override void SetCursor()
    {
        leftCursor = FindObjectOfType<Cursor8>();
        rightCursor = FindObjectOfType<Cursor9>();
    }
<<<<<<< HEAD
=======
    void Update()
    {
        if (
            Fear.objInstance.hasDied &&
            CheckPoint.objInstance.checkPointName == CheckPoint.objInstance.colliderName + " (1)"
        ) TurnOnLeftCursor();
    }
>>>>>>> programming
}