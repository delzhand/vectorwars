using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Checking version...");
        StartCoroutine(GetVersionNumber());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator GetVersionNumber()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://vector-wars.lndo.site/api/v1");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            try
            {
                BasicResponse r = JsonUtility.FromJson<BasicResponse>(www.downloadHandler.text);
                if (updateFound(r))
                {
                    Debug.Log("Update data available! v" + r.VersionNumber() + " found. Updating...");
                    PlayerPrefs.SetString("version", r.VersionNumber());
                    Debug.Log("Update complete!");
                }
                else
                {
                    Debug.Log("Up to date! (v" + r.VersionNumber() + ")");
                }
            }
            catch(Exception e)
            {
                Debug.Log(e.Message);
            }
        }
    }

    public IEnumerator GetVectorCores()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://vector-wars.lndo.site/api/v1/list-cores");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }

    }

    private bool updateFound(BasicResponse r)
    {
        string localVersion = PlayerPrefs.GetString("version", "0.1.0");
        string[] lv = localVersion.Split('.');
        return int.Parse(lv[0]) < r.data_major_version || int.Parse(lv[1]) < r.data_minor_version || int.Parse(lv[2]) < r.patch_version;
    }
}
