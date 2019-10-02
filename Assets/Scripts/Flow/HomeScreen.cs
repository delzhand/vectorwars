using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class HomeScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StateManager sm = StateManager.GetController();

        GameObject g = (GameObject)Instantiate(Resources.Load("Interface/Main Menu"), sm.UiRoot, false);
        g.name = "Main Menu";

        PlayerData pd = sm.pdata;

        transform.Find("Currency/Cores Label/Text").GetComponent<Text>().text = "Cores: " + pd.Cores;
        transform.Find("Currency/Bits Label/Text").GetComponent<Text>().text = "Bits: " + pd.Bits;
        transform.Find("Currency/Circuits Label/Text").GetComponent<Text>().text = "Circuits: " + pd.Circuits;
    }
}
