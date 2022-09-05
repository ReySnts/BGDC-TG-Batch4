public class HorClamp9 : Clamp
{
    protected override void SetCursor()
    {
        leftCursor = FindObjectOfType<Cursor11>();
        rightCursor = FindObjectOfType<Cursor12>();
    }
<<<<<<< HEAD
=======
    void Update()
    {
        if (
            Fear.objInstance.hasDied && 
            CheckPoint.objInstance.checkPointName == CheckPoint.objInstance.colliderName + " (3)"
        ) TurnOnLeftCursor();
    }
>>>>>>> programming
}