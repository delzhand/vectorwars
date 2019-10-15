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
        transform.Find("Grid/Button5/Text").GetComponent<Text>().text = "Start Lerp";
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

    private int direction = -1;
    private int cpos = 0;
    public void Button5()
    {
        //Console.Log("Menus Hidden.");
        //StateManager.GetController().UiRoot.Find("Home/Player Data").GetComponent<Animator>().Play("OutTop");
        //StateManager.GetController().UiRoot.Find("Home/Main Menu").GetComponent<Animator>().Play("OutBottom");
        
        if (cpos == 0)
        {
            cpos += direction;
        }
        else if (cpos == -1)
        {
            cpos = 0;
            direction = 1;
        }
        else
        {
            cpos = 0;
            direction = -1;
        }
        foreach(CameraController c in FindObjectsOfType<CameraController>())
        {
            CameraPosition p = CameraPosition.Center;
            if (cpos == -1)
            {
                p = CameraPosition.Left;
            }
            if (cpos == 1)
            {
                p = CameraPosition.Right;
            }
            Console.Log("Camera position = " + p.ToString());
            //c.SetTarget(new Vector3(-Random.Range(0, 8), 0, Random.Range(0, 8)), .35f);
            c.SetAngle(p, .25f);
        }
    }

}
