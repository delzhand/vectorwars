using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Transform t = GameObject.FindGameObjectWithTag("UiRoot").transform;
        GameObject g = (GameObject)Instantiate(Resources.Load("Interface/Main Menu"), t, false);
        g.name = "Main Menu";
    }
}
