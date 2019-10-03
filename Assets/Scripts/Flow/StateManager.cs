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

    public void Start()
    {
        pdata.DemoInit();
        //pdata = PlayerData.Load();

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

    public static void DismissSubscreens()
    {
        foreach (GameObject off in GameObject.FindGameObjectsWithTag("Subscreen"))
        {
            Animator a = off.GetComponent<Animator>();
            if (a != null)
            {
                off.GetComponent<Animator>().StopPlayback();
                off.GetComponent<Animator>().Play("OutLeft");
                RuntimeAnimatorController r = off.GetComponent<Animator>().runtimeAnimatorController;
                float transitionOffTime = 0;
                foreach(AnimationClip ac in r.animationClips)
                {
                    if (ac.name == "OutLeft")
                    {
                        transitionOffTime = ac.length - .01f;
                    }
                }
                Destroy(off, transitionOffTime);
            }
            else
            {
                Destroy(off);
            }

        }
    }
}
