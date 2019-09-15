using System.Collections;
using System.Collections.Generic;
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
        string currentVersion = PlayerPrefs.GetString("version_id", "1");
        apiManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<APIManager>();
        apiManager.success += VersionCheckComplete;
        apiManager.Request("/api/v1/get-updates/" + currentVersion, true);
    }

    public void VersionCheckComplete(string response)
    {
        apiManager.success -= VersionCheckComplete;
        VersionUpdateList updateList = JsonUtility.FromJson<VersionUpdateList>(response);
        if (updateList.updates.Count == 0)
        {
            StartButton.GetComponent<Button>().interactable = true;
            StartButton.GetComponent<Button>().onClick.AddListener(GameObject.FindGameObjectWithTag("GameController").GetComponent<StateManager>().GoToHome);
            StartButton.GetComponent<Text>().enabled = true;
        }
        else
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<StateManager>().GoToUpdate(response);
        }
    }

}
