using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScreen : MonoBehaviour
{
    public VersionUpdateList updateList;
    public GameObject versionUpdateText;
    private APIManager apiManager;
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        updateVersionUpdateText();
        apiManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<APIManager>();
        apiManager.success += VersionUpdateComplete;
        apiManager.Request("/api/v1/get-update/" + updateList.updates[index].id);
    }

    public void VersionUpdateComplete(string response)
    {
        PlayerPrefs.SetString("version_id", updateList.updates[index].id);
        PlayerPrefs.SetString("version_label", updateList.updates[index].label);
        index++;
        if (index <= updateList.updates.Count - 1)
        {
            updateVersionUpdateText();
            apiManager.Request("/api/v1/get-update/" + updateList.updates[index].id);
        }
        else
        {
            apiManager.success -= VersionUpdateComplete;
            GameObject.FindGameObjectWithTag("GameController").GetComponent<StateManager>().GoToHome();
        }
    }

    private void updateVersionUpdateText()
    {
        versionUpdateText.GetComponent<Text>().text = getVersionUpdateText();
    }

    private string getVersionUpdateText()
    {
        return "Downloading update " + updateList.updates[index].label + "\n" + (index + 1) + " of " + updateList.updates.Count;
    }
}
