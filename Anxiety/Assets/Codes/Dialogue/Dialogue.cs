public class Dialogue : Sentence
{
    public static Dialogue objInstance = null;
    public string[] characterName = null;
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