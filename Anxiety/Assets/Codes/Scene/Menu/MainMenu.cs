using System.Collections.Generic;
using UnityEngine;
public class MainMenu : MonoBehaviour
{
    List<GameObject> menuUIList = new List<GameObject>();
    public void Play()
    {
        SceneManagement.NextScene();
    }
    public void Credits()
    {
        SceneManagement.ToCredits();
    }
    public void Exit()
    {
        Application.Quit();
    }
    void OnEnable()
    {
        RegisterMenu();
        OpenMain();
    }
    void RegisterMenu()
    {
        menuUIList.Add(GameObject.Find("Main"));
        menuUIList.Add(GameObject.Find("Settings"));
        menuUIList.Add(GameObject.Find("Bonus"));
    }
    public void OpenMain()
    {
        foreach (GameObject menuUI in menuUIList)
        {
            if (menuUI.name == "Main") menuUI.SetActive(true);
            else menuUI.SetActive(false);
        }
    }
    public void OpenSettings()
    {
        foreach (GameObject menuUI in menuUIList)
        {
            if (menuUI.name == "Settings") menuUI.SetActive(true);
            else menuUI.SetActive(false);
        }
    }
    public void OpenBonus()
    {
        foreach (GameObject menuUI in menuUIList)
        {
            if (menuUI.name == "Bonus") menuUI.SetActive(true);
            else menuUI.SetActive(false);
        }
    }
}