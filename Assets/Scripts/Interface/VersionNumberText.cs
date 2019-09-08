using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VersionNumberText : MonoBehaviour
{
    public void Start()
    {
        GetComponent<Text>().text = "Version: " + PlayerPrefs.GetString("version");
    }

    public void UpdateVersionNumber()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<APIManager>().apiCallFinished -= UpdateVersionNumber;
        GetComponent<Text>().text = "Version: " + PlayerPrefs.GetString("version");
    }
}
