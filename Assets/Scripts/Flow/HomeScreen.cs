using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class HomeScreen : MonoBehaviour
{
    public Text CurrencyText;
    public Text RosterText;
    public Text SkillsText;

    // Start is called before the first frame update
    void Start()
    {
        StateManager sm = StateManager.GetController();

        GameObject g = (GameObject)Instantiate(Resources.Load("Interface/Main Menu"), sm.UiRoot, false);
        g.name = "Main Menu";

        PlayerData pd = sm.pdata;
        CurrencyText.text = "Circuits: " + pd.Circuits
            + "\nBits: " + pd.Bits
            + "\nCores: " + pd.Cores;

        Dictionary<int, VectorCore> library = VectorCore.GetLibrary();
        int counter = 0;
        foreach (VectorLocal vl in pd.VectorLocals)
        {
            VectorTile.Create(vl, Vector2.zero, GameObject.FindGameObjectWithTag("UiRoot").transform.Find("Home/Roster").transform);
            counter++;
        }
        GameObject.FindGameObjectWithTag("UiRoot").transform.Find("Home/Roster").GetComponent<VectorTileGrid>().InitGrid();

    }
}
