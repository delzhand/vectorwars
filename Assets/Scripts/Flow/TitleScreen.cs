using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    private APIManager apiManager;

    // Start is called before the first frame update
    void Start()
    {
        apiManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<APIManager>();
        apiManager.apiCallFinished += GameObject.Find("Version Number").GetComponent<VersionNumberText>().UpdateVersionNumber;
        apiManager.apiCallFinished += APICheckDone;
        apiManager.StartVersionCheck();
    }

    public void APICheckDone()
    {
        Destroy(GameObject.Find("API Indicator"));
        GameObject.Find("Start").GetComponent<Text>().enabled = true;
        GameObject.Find("Start").GetComponent<Button>().interactable = true;
        GameObject.Find("Start").GetComponent<Button>().onClick.AddListener(GameObject.FindGameObjectWithTag("GameController").GetComponent<StateManager>().GoToHome);
        apiManager.apiCallFinished -= APICheckDone;
    }

}
