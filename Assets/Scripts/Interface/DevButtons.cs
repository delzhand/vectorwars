using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DevButtons : MonoBehaviour
{
    public void Update()
    {
        transform.Find("Grid/Button1/Text").GetComponent<Text>().text = GameObject.Find("UI/Full-size Panel/Console").activeSelf ? "Hide Console" : "Show Console";
        transform.Find("Grid/Button2/Text").GetComponent<Text>().text = "Delete Prefs";
        transform.Find("Grid/Button3/Text").GetComponent<Text>().text = "DemoInit PData";
        transform.Find("Grid/Button4/Text").GetComponent<Text>().text = PlayerPrefs.GetInt("OfflineMode", 0) == 0 ? "Offline Mode" : "Online Mode";
        transform.Find("Grid/Button5/Text").GetComponent<Text>().text = "N/A";
    }


    public void Button1()
    {
        bool isActive = GameObject.Find("UI/Full-size Panel/Console").activeSelf;
        GameObject.Find("UI/Full-size Panel/Console").SetActive(!isActive);
    }

    public void Button2()
    {
        PlayerPrefs.DeleteAll();
        Console.Log("PlayerPrefs deleted.");
    }

    public void Button3()
    {
        StateManager.GetController().pdata = PlayerData.DemoInit();
        Console.Log("Demo values set for PData.");
    }

    public void Button4()
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

    public void Button5()
    {
        Console.Log("Nothing assigned.");
    }

}
