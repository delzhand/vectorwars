using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Screen
{
    title,
    update,
    home
}

public class FlowManager : MonoBehaviour
{
    public Screen screen;
    private APIManager apiManager;

    public void Start()
    {
        screen = Screen.title;
        apiManager = GetComponent<APIManager>();
        apiManager.apiCallFinished += GameObject.Find("Version Number").GetComponent<VersionNumberText>().UpdateVersionNumber;
        apiManager.apiCallFinished += APICheckDone;
        apiManager.StartVersionCheck();
    }

    public void APICheckDone()
    {
        Destroy(GameObject.Find("API Indicator"));
        GameObject.Find("Start").GetComponent<Text>().enabled = true;
        apiManager.apiCallFinished -= APICheckDone;
    }
}
