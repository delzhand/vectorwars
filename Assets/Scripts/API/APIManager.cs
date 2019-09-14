using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{
    public delegate void APICallFinished(string response);
    public event APICallFinished finish;
    public event APICallFinished success;
    public event APICallFinished failure;

    public bool offline = false;
    public string onlineUrl = "http://vector-wars.lndo.site";
    public string offlineUrl = "http://localhost:32779";

    private string baseUrl;

    public void Start()
    {
        baseUrl = offline ? offlineUrl : onlineUrl;
    }

    //public void VersionCheck()
    //{
    //    Console.Log("Checking version...");
    //    ShowFullScreenIndicator();
    //    StartCoroutine(CheckForUpdates());
    //}

    public void Request(string target, bool fullScreenIndicator = false)
    {
        if (fullScreenIndicator)
        {
            ShowFullScreenIndicator();
        }
        StartCoroutine(MakeApiCall(target));
    }

    private IEnumerator MakeApiCall(string target)
    {
        string requestUrl = baseUrl + target;
        UnityWebRequest www = UnityWebRequest.Get(requestUrl);
        Console.Log("Contacting " + requestUrl);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Console.Log("Communication error: " + www.error);
            if (finish != null)
            {
                finish(www.downloadHandler.text);
            }
            if (failure != null)
            {
                failure(www.downloadHandler.text);
            }
        }
        else
        {
            Console.Log("Response acquired.");
            if (finish != null)
            {
                finish(www.downloadHandler.text);
            }
            if (success != null)
            {
                success(www.downloadHandler.text);
            }
        }
    }

    //public IEnumerator CheckForUpdates()
    //{
    //    string currentVersion = PlayerPrefs.GetString("version_id", "1");
    //    string requestUrl = baseUrl + "/api/v1/get-updates/" + currentVersion;
    //    UnityWebRequest www = UnityWebRequest.Get(requestUrl);
    //    Console.Log("Contacting " + requestUrl);
    //    yield return www.SendWebRequest();

    //    if (www.isNetworkError || www.isHttpError)
    //    {
    //        Console.Log("Communication error: " + www.error);
    //        finish();
    //        failure();
    //    }
    //    else
    //    {
    //        PlayerPrefs.SetString("pending_updates", www.downloadHandler.text);
    //        finish();
    //        success();
    //    }
    //}

    //public IEnumerator GetUpdate(int update_id)
    //{

    //}

    //public IEnumerator DownloadFile(string url, string savePath)
    //{
    //    UnityWebRequest www = UnityWebRequest.Get(url);
    //    yield return www.SendWebRequest();

    //    if (www.isNetworkError || www.isHttpError)
    //    {
    //        Debug.Log(www.error);
    //    }
    //    else
    //    {
    //        try
    //        {
    //            File.WriteAllBytes(savePath, www.downloadHandler.data);
    //            Console.Log(url + " downloaded.");
    //        }
    //        catch (Exception e)
    //        {
    //            Console.Log(e.Message);
    //        }
    //    }

    //}

    //public IEnumerator GetVectorCores()
    //{
    //    Console.Log("Downloading...");
    //    UnityWebRequest www = UnityWebRequest.Get("http://vector-wars.lndo.site/api/v1/list-cores");
    //    yield return www.SendWebRequest();

    //    if (www.isNetworkError || www.isHttpError)
    //    {
    //        Console.Log(www.error);
    //    }
    //    else
    //    {
    //        try
    //        {
    //            Console.Log("Applying update...");
    //            if (!Directory.Exists(Application.persistentDataPath + "/GameData"))
    //            {
    //                Console.Log("No Game Data Found. Initializing...");
    //                Directory.CreateDirectory(Application.persistentDataPath + "/GameData");
    //            }
    //            File.WriteAllText(Application.persistentDataPath + "/GameData/VectorCores.json", www.downloadHandler.text);
    //            VectorCoreList vcores = JsonUtility.FromJson<VectorCoreList>(www.downloadHandler.text);
    //            if (!Directory.Exists(Application.persistentDataPath + "/GameData/Sprites"))
    //            {
    //                Directory.CreateDirectory(Application.persistentDataPath + "/GameData/Sprites");
    //            }
    //            foreach (VectorCore v in vcores.vector_core_list)
    //            {
    //                StartCoroutine(DownloadFile(v.sprite, Application.persistentDataPath + "/GameData/Sprites/" + v.name + "_sprite.png"));
    //                Console.Log(v.name + " downloaded.");
    //            }
    //        }
    //        catch(Exception e)
    //        {
    //            Console.Log(e.Message);
    //        }
    //    }
    //}

    //private bool updateFound(BasicResponse r)
    //{
    //    string localVersion = PlayerPrefs.GetString("version", "0.1.0");
    //    string[] lv = localVersion.Split('.');
    //    return int.Parse(lv[0]) < r.data_major_version || int.Parse(lv[1]) < r.data_minor_version || int.Parse(lv[2]) < r.patch_version;
    //}

    //private void doUpdate(BasicResponse r)
    //{
    //    Console.Log("Update data available! v" + r.VersionNumber() + " found. Updating...");
    //    PlayerPrefs.SetString("version", r.VersionNumber());
    //    StartCoroutine(GetVectorCores());
    //    Console.Log("Update complete!");
    //}

    public void ShowFullScreenIndicator()
    {
        Transform t = GameObject.FindGameObjectWithTag("UiRoot").transform;
        GameObject g = (GameObject)Instantiate(Resources.Load("Interface/API Indicator"), new Vector3(0, 0, 0), Quaternion.identity);
        g.name = "API Indicator";
        g.transform.SetParent(t, false);
        finish += RemoveFullScreenIndicator;
    }

    public void RemoveFullScreenIndicator(string response)
    {
        Transform t = GameObject.FindGameObjectWithTag("UiRoot").transform;
        Destroy(t.Find("API Indicator").gameObject);
        finish -= RemoveFullScreenIndicator;
    }

}
