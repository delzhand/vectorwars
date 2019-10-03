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
        PlayerData pd = sm.pdata;

        transform.Find("Player Data/Currency/Cores Label/Text").GetComponent<Text>().text = "Cores: " + pd.Cores;
        transform.Find("Player Data/Currency/Bits Label/Text").GetComponent<Text>().text = "Bits: " + pd.Bits;
        transform.Find("Player Data/Currency/Circuits Label/Text").GetComponent<Text>().text = "Circuits: " + pd.Circuits;
        transform.Find("Player Data/PlayerName Label/Text").GetComponent<Text>().text = pd.PlayerName;
    }
}
