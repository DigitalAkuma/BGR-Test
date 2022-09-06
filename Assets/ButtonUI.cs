using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.Runtime.InteropServices;



public class ButtonUI : MonoBehaviour
{

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

public void LoadScene(string newScene)
    {
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(newScene);
        SceneManager.UnloadScene(PlayerPrefs.GetString("LastScene"));
    }

    public void OpenURL(string link)
    {
        if (link == "Instagram")
        {
            Application.OpenURL("https://www.instagram.com/barbiegirlsrewritten/");
        }
        else if (link == "Twitter")
        {
            Application.OpenURL("https://twitter.com/bgrewritten?s=21");
        }
        else if (link == "Discord")
        {
            Application.OpenURL("https://discord.com/invite/JccZBXkGa7");
        }
        else if (link == "TikTok")
        {
            Application.OpenURL("https://vm.tiktok.com/TTPdrLNXjm/");
        }
        else if (link == "Youtube")
        {
            Application.OpenURL("https://youtube.com/channel/UCx4xDDEp-xsnEwCV58iTMTA");
        }
        else if (link == "ReleaseNotes")
        {
            Application.OpenURL("https://docs.google.com/document/d/179mzkzjbO9mZwmKzU1Gq80ZMAUaYIQcLgozXAX5QIqQ/edit?usp=sharing");
        }   
    }

    public void BackScene()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("LastScene"));
    }

    public void CloseApp()
    {
        Application.Quit();
    }

    public void MinimizeApp()
    {
        ShowWindow(GetActiveWindow(), 2);
    }

}
