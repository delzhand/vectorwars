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

    public bool UseAPI = false;
    public string APIUrl = "";

    public void Start()
    {
    }

    public void Request(string target, bool fullScreenIndicator = false)
    {
        if (fullScreenIndicator)
        {
            ShowFullScreenIndicator();
        }

        if (UseAPI)
        {
            StartCoroutine(MakeApiCall(target));
        }
        else
        {
            // Use mocks
            string requestUrl = APIUrl + target;
            string mockResponse = getMockResponse(target);
            Console.Log("Contacting " + requestUrl + " [MOCK]");
            if (finish != null)
            {
                finish(mockResponse);
            }
            if (success != null)
            {
                success(mockResponse);
            }
        }
    }

    private IEnumerator MakeApiCall(string target)
    {
        string requestUrl = APIUrl + target;

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

    private string getMockResponse(string target)
    {
        switch (target)
        {
            case "/api/v1/get-updates/1":
                return "{\"updates\":[{\"id\":\"3\",\"label\":\"0.1.2\"},{\"id\":\"4\",\"label\":\"0.1.3\"}]}";
            default:
                return "{}";
        }
    }
}
