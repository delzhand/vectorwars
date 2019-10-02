using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VersionNumberText : MonoBehaviour
{
    public void Start()
    {
        UpdateVersionNumber();
    }

    public void UpdateVersionNumber()
    {
        GetComponent<Text>().text = "Version: " + PlayerPrefs.GetString("version_label", "0.1.0");
    }
}
