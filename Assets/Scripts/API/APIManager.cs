using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{
    public delegate void APICallFinished(string response);
    public delegate void DownloadProgress(float percent);
    public delegate void DownloadFinished(AssetBundle response);
    public event APICallFinished requestFinish;
    public event APICallFinished requestSuccess;
    public event APICallFinished requestFailure;
    public event DownloadProgress downloadProgress;
    public event DownloadFinished downloadFinish;
    public event DownloadFinished downloadSuccess;
    public event DownloadFinished downloadFailure;

    public string APIDomain = "";

    private static string getURL(string id, string[] urlParams)
    {
        switch (id)
        {
            case "getUpdates":
                return "/api.php?route=get-updates&current=" + urlParams[0];
            case "getUpdate":
                return "/api.php?route=get-update&update=" + urlParams[0];
            default:
                return "";
        }
    }

    public void Download(string target)
    {
        StartCoroutine(MakeDownloadCall(target));
    }

    public void Request(string target, string urlParam, bool fullScreenIndicator = false)
    {
        Request(target, new string[] { urlParam }, fullScreenIndicator);
    }

    public void Request(string target, string[] urlParams, bool fullScreenIndicator = false)
    {
        target = getURL(target, urlParams);

        if (fullScreenIndicator)
        {
            ShowFullScreenIndicator();
        }

        if (PlayerPrefs.GetInt("OfflineMode", 0) == 0)
        {
            StartCoroutine(MakeApiCall(target));
        }
        else
        {
            // Use mocks
            string requestUrl = APIDomain + target;
            string mockResponse = getMockResponse(target);
            Console.Log("Contacting " + requestUrl + " [MOCK]");
            if (requestFinish != null)
            {
                requestFinish(mockResponse);
            }
            if (requestSuccess != null)
            {
                requestSuccess(mockResponse);
            }
        }
    }

    private IEnumerator MakeApiCall(string target)
    {
        string requestUrl = APIDomain + target;
        UnityWebRequest www = UnityWebRequest.Get(requestUrl);
        Console.Log("Contacting " + requestUrl);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Console.Log("Communication error: " + www.error);
            if (requestFinish != null)
            {
                requestFinish(www.downloadHandler.text);
            }
            if (requestFailure != null)
            {
                requestFailure(www.downloadHandler.text);
            }
        }
        else
        {
            Console.Log("Response acquired.");
            if (requestFinish != null)
            {
                requestFinish(www.downloadHandler.text);
            }
            if (requestSuccess != null)
            {
                requestSuccess(www.downloadHandler.text);
            }
        }
    }

    private IEnumerator MakeDownloadCall(string target)
    {
        string requestUrl = APIDomain + target;
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(requestUrl);
        UnityWebRequestAsyncOperation operation = www.SendWebRequest();
        Console.Log("Downloading " + requestUrl);
        while (!operation.isDone)
        {
            if (downloadProgress != null)
            {
                downloadProgress(www.downloadProgress * 100f);
            }
            yield return null;
        }

        if (www.isNetworkError || www.isHttpError)
        {
            Console.Log("Communication error: " + www.error);
            if (downloadFinish != null)
            {
                downloadFinish(null);
            }
            if (downloadFailure != null)
            {
                downloadFailure(null);
            }
        }
        else
        {
            Console.Log("Download completed.");
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            if (downloadFinish != null)
            {
                downloadFinish(bundle);
            }
            if (downloadSuccess != null)
            {
                downloadSuccess(bundle);
            }
        }
    }

    public void ShowFullScreenIndicator()
    {
        Transform t = GameObject.FindGameObjectWithTag("UiRoot").transform;
        GameObject g = (GameObject)Instantiate(Resources.Load("Interface/API Indicator"), new Vector3(0, 0, 0), Quaternion.identity);
        g.name = "API Indicator";
        g.transform.SetParent(t, false);
        requestFinish += RemoveFullScreenIndicator;
    }

    public void RemoveFullScreenIndicator(string response)
    {
        Transform t = GameObject.FindGameObjectWithTag("UiRoot").transform;
        Destroy(t.Find("API Indicator").gameObject);
        requestFinish -= RemoveFullScreenIndicator;
    }

    private string getMockResponse(string target)
    {
        switch (target)
        {
            default:
                return "{}";
        }
    }
}
