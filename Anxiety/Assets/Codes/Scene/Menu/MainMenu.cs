using System.Collections.Generic;
using UnityEngine;
public class MainMenu : MonoBehaviour
{
    List<GameObject> menuUIList = new List<GameObject>();
    public void Play()
    {
        foreach (GameObject menuUI in menuUIList) menuUI.SetActive(false);
        NextScene.objInstance.StartLoading("Level");
    }
    public void Credits()
    {
        SceneManagement.ToCredits();
    }
    public void AskToExit()
    {
        foreach(GameObject menuUI in menuUIList)
        {
            if (menuUI.name == "Exit") menuUI.SetActive(true);
            else menuUI.SetActive(false);
        }
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
        menuUIList.Add(GameObject.Find("Exit"));
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