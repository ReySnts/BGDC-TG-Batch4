public class Guideline : Sentence
{
    public static Guideline objInstance = null;
    void Awake()
    {
        if (
            objInstance == null &&
            (
                SceneManagement.GetCurrentScene() == 1 ||
                SceneManagement.GetCurrentScene() == 4
            )
        ) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
    }
}