using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevButtons : MonoBehaviour
{
    public void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Console.Log("PlayerPrefs deleted.");
    }

    public void ToggleConsole()
    {
        bool isActive = GameObject.Find("UI/Full-size Panel/Console").activeSelf;
        GameObject.Find("UI/Full-size Panel/Console").SetActive(!isActive);
    }

    public void LogPlayerData()
    {
        // Yes, debug. Console isn't useful for huge strings.
        Debug.Log(StateManager.GetController().pdata.ToString());
    }

    public void OfflineMode()
    {
        int offline = PlayerPrefs.GetInt("OfflineMode", 0);
        if (offline == 0)
        {
            Console.Log("Offline Mode active.");
            offline = 1;
        }
        else
        {
            Console.Log("Online Mode active.");
            offline = 0;
        }
        PlayerPrefs.SetInt("OfflineMode", offline);
    }

    public void X()
    {
        Console.Log("Nothing assigned.");
    }
}
