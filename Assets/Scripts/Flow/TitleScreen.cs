using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    private APIManager apiManager;
    public GameObject versionNumberText;
    public GameObject StartButton;

    // Start is called before the first frame update
    void Start()
    {
        versionNumberText.GetComponent<VersionNumberText>().UpdateVersionNumber();
        apiManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<APIManager>();
    }

    public void StartButtonClick()
    {
        string currentVersion = PlayerPrefs.GetString("version_id", "1");
        apiManager.requestSuccess += VersionCheckComplete;
        apiManager.Request("getUpdates", currentVersion, true);
    }

    public void VersionCheckComplete(string response)
    {
        apiManager.requestSuccess -= VersionCheckComplete;
        VersionUpdateList updateList = JsonUtility.FromJson<VersionUpdateList>(response);


        if (updateList.updates.Count == 0)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<StateManager>().GoToHome();
        }
        else
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<StateManager>().GoToUpdate(response);
        }
    }



}
