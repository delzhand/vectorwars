using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{
    public delegate void APICallFinished();
    public event APICallFinished apiCallFinished;

    public void StartVersionCheck()
    {
        Transform t = GameObject.Find("UI/Full-size Panel/16:9 Panel").transform;
        GameObject g = (GameObject)Instantiate(Resources.Load("Interface/API Indicator"), new Vector3(0, 0, 0), Quaternion.identity);
        g.name = "API Indicator";
        g.transform.SetParent(t, false);
        Debug.Log("Checking version...");
        StartCoroutine(GetVersionNumber());
    }

    public IEnumerator GetVersionNumber()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://vector-wars.lndo.site/api/v1");
        //www = UnityWebRequest.Get("http://localhost:32783/api/v1");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Communication error:" + www.error);

        }
        else
        {
            BasicResponse r = null;
            try
            {
                r = JsonUtility.FromJson<BasicResponse>(www.downloadHandler.text);
            }
            catch(Exception e)
            {
                Debug.Log(e.Message);
            }
            if (updateFound(r))
            {
                doUpdate(r);
            }
            else
            {
                Debug.Log("Up to date! (v" + r.VersionNumber() + ")");
                apiCallFinished();
                //foreach (SpriteRenderer s in indicator.GetComponentsInChildren<SpriteRenderer>())
                //{
                //    s.gameObject.AddComponent<FadeOut>().SetDuration(.2f);
                //}
                //Destroy(indicator, .2f);
            }

        }
    }

    public IEnumerator DownloadFile(string url, string savePath)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            try
            {
                File.WriteAllBytes(savePath, www.downloadHandler.data);
                Debug.Log(url + " downloaded.");
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }

    }

    public IEnumerator GetVectorCores()
    {
        Debug.Log("Downloading...");
        UnityWebRequest www = UnityWebRequest.Get("http://vector-wars.lndo.site/api/v1/list-cores");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            try
            {
                Debug.Log("Applying update...");
                if (!Directory.Exists(Application.persistentDataPath + "/GameData"))
                {
                    Debug.Log("No Game Data Found. Initializing...");
                    Directory.CreateDirectory(Application.persistentDataPath + "/GameData");
                }
                File.WriteAllText(Application.persistentDataPath + "/GameData/VectorCores.json", www.downloadHandler.text);
                VectorCoreList vcores = JsonUtility.FromJson<VectorCoreList>(www.downloadHandler.text);
                if (!Directory.Exists(Application.persistentDataPath + "/GameData/Sprites"))
                {
                    Directory.CreateDirectory(Application.persistentDataPath + "/GameData/Sprites");
                }
                foreach (VectorCore v in vcores.vector_core_list)
                {
                    StartCoroutine(DownloadFile(v.sprite, Application.persistentDataPath + "/GameData/Sprites/" + v.name + "_sprite.png"));
                    Debug.Log(v.name + " downloaded.");
                }
            }
            catch(Exception e)
            {
                Debug.Log(e.Message);
            }
        }
    }

    private bool updateFound(BasicResponse r)
    {
        string localVersion = PlayerPrefs.GetString("version", "0.1.0");
        string[] lv = localVersion.Split('.');
        return int.Parse(lv[0]) < r.data_major_version || int.Parse(lv[1]) < r.data_minor_version || int.Parse(lv[2]) < r.patch_version;
    }

    private void doUpdate(BasicResponse r)
    {
        Debug.Log("Update data available! v" + r.VersionNumber() + " found. Updating...");
        PlayerPrefs.SetString("version", r.VersionNumber());
        StartCoroutine(GetVectorCores());
        Debug.Log("Update complete!");
    }
}
