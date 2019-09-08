using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum Screen
{
    title,
    update,
    home
}

public class StateManager : MonoBehaviour
{
    public Screen screen;

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
        LoadScreen(Screen.title);
    }


    public void GoToHome()
    {
        LoadScreen(Screen.home);
    }

    private void LoadScreen(Screen s)
    {
        Transform toDestroy = GameObject.FindWithTag("UiRoot").transform.Find(screen.ToString());
        if (toDestroy)
        {
            Destroy(toDestroy.gameObject);
        }
        screen = s;
        GameObject g = (GameObject)Instantiate(Resources.Load("Interface/" + s.ToString()), new Vector3(0, 0, 0), Quaternion.identity);
        g.name = s.ToString();
        Transform uiRoot = GameObject.FindWithTag("UiRoot").transform;
        g.transform.SetParent(uiRoot, false);

    }
}
