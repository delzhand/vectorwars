using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
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
        updateModalText();
        apiManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<APIManager>();
    }

    public void StartUpdates()
    {
        Destroy(transform.Find("Modal").gameObject);
        apiManager.downloadProgress += DownloadProgress;
        apiManager.downloadSuccess += DownloadReceived;
        apiManager.Download(updateList.updates[index].path);
        updateVersionUpdateText(0);
    }

    public void DownloadProgress(float progress)
    {
        updateVersionUpdateText(progress);
        //Console.Log("download progress: " + progress);
    }

    public void DownloadReceived(AssetBundle bundle)
    {
        DataManager.UpdateFromBundle(bundle);
        PlayerPrefs.SetString("version_id", updateList.updates[index].id);
        PlayerPrefs.SetString("version_label", updateList.updates[index].label);
        index++;
        if (index <= updateList.updates.Count - 1)
        {
            updateVersionUpdateText(100f);
            apiManager.Download(updateList.updates[index].path);
        }
        else
        {
            apiManager.downloadSuccess -= DownloadReceived;
            apiManager.downloadProgress -= DownloadProgress;
            transform.Find("UpdateText/Done Button").gameObject.SetActive(true);
        }
    }

    public void UpdatesFinished()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<StateManager>().GoToHome();
    }

    private void updateModalText()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Update data found.\nTotal size: ");
        int bytes = 0;
        foreach(VersionUpdate vu in updateList.updates)
        {
            bytes += vu.size;
        }
        sb.AppendLine(formatBytes(bytes));
        sb.AppendLine("Begin download?");
        transform.Find("Modal/Background/Text").GetComponent<Text>().text = sb.ToString();
    }

    private void updateVersionUpdateText(float percent)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Downloading update " + updateList.updates[index].label + "\n" + (index + 1) + " of " + updateList.updates.Count);

        float thisBytes = percent / 100f * updateList.updates[index].size;
        float totalBytes = 0;
        foreach (VersionUpdate vu in updateList.updates)
        {
            totalBytes += vu.size;
        }
        float downloadedBytes = 0;
        for (int i = 0; i < index; i++)
        {
            downloadedBytes += updateList.updates[i].size;
        }
        downloadedBytes += thisBytes;
        float totalPercent = downloadedBytes / totalBytes * 100f;

        ProgressBar current = transform.Find("UpdateText/CurrentProgress").GetComponent <ProgressBar>();
        ProgressBar total = transform.Find("UpdateText/TotalProgress").GetComponent<ProgressBar>();

        current.max = updateList.updates[index].size;
        current.SetValue(thisBytes);
        total.max = totalBytes;
        total.SetValue(downloadedBytes);

        versionUpdateText.SetActive(true);
        versionUpdateText.GetComponent<Text>().text = sb.ToString();
    }


    private string formatBytes(int bytes)
    {
        if (bytes * 0.000001f < 10)
        {
            return Mathf.RoundToInt(bytes * .001f) + "KB";
        }
        else
        {
            return Mathf.RoundToInt(bytes * .000001f) + "MB";
        }
    }
}
