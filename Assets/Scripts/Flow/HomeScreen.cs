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
        StringBuilder sb = new StringBuilder();
        foreach (VectorLocal vl in pd.VectorLocals)
        {
            sb.Append(library[vl.Core].name);
            sb.Append(" (");
            for (int i = 0; i < vl.Rank; i++)
            {
                sb.Append("*");
            }
            sb.Append(") ");
            sb.Append("Lv " + vl.Level);
            sb.Append("\n");
        }
        RosterText.text = sb.ToString();
    }
}
