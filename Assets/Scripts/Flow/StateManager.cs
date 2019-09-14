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
    public bool resetPlayerPrefs = false;


    public void Start()
    {
        if (resetPlayerPrefs)
        {
            PlayerPrefs.DeleteAll();
        }
        DontDestroyOnLoad(gameObject);
        LoadScreen(Screen.Title);
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
        Transform toDestroy = GameObject.FindWithTag("UiRoot").transform.Find(screen.ToString());
        if (toDestroy)
        {
            Console.Log("Unloading " + screen.ToString() + " Screen element.");
            Destroy(toDestroy.gameObject);
        }
        screen = s;
        Transform uiRoot = GameObject.FindWithTag("UiRoot").transform;
        GameObject g = (GameObject)Instantiate(Resources.Load("Interface/" + s.ToString() + " Screen"), uiRoot, false);
        g.name = s.ToString();
        Console.Log("Loaded " + s.ToString() + " Screen element.");
        return g;
    }
}
