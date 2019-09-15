using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum Screen
{
    Title,
    Update,
    Home
}

public class StateManager : MonoBehaviour
{
    public Screen screen;
    public PlayerData pdata;
    public Transform UiRoot;
    public bool resetPlayerPrefs = false;


    public void Start()
    {
        if (resetPlayerPrefs)
        {
            PlayerPrefs.DeleteAll();
        }
        pdata = new PlayerData();
        pdata.DemoInit();

        DontDestroyOnLoad(gameObject);
        LoadScreen(Screen.Title);
    }

    public static StateManager GetController()
    {
        return GameObject.FindGameObjectWithTag("GameController").GetComponent<StateManager>();
    }

    public void GoToHome()
    {
        LoadScreen(Screen.Home);
    }

    public void GoToUpdate(string response)
    {
        GameObject g = LoadScreen(Screen.Update);
        g.GetComponent<UpdateScreen>().updateList = JsonUtility.FromJson<VersionUpdateList>(response);
    }

    private GameObject LoadScreen(Screen s)
    {
        Transform toDestroy = UiRoot.Find(screen.ToString());
        if (toDestroy)
        {
            Console.Log("Unloading " + screen.ToString() + " Screen element.");
            Destroy(toDestroy.gameObject);
        }
        screen = s;
        GameObject g = (GameObject)Instantiate(Resources.Load("Interface/" + s.ToString() + " Screen"), UiRoot, false);
        g.name = s.ToString();
        Console.Log("Loaded " + s.ToString() + " Screen element.");
        return g;
    }
}
