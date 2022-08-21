using System.Collections;
using UnityEngine;
public class CreditsMenu : MonoBehaviour
{
    IEnumerator HoldMainMenu()
    {
        yield return new WaitForSeconds(20f);
        SceneManagement.ToMainMenu();
    }
    void Start()
    {
        Time.timeScale = 1f;
        StartCoroutine(HoldMainMenu());
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) SceneManagement.ToMainMenu();
    }
}