using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadAssignment : MonoBehaviour
{
	public int SquadIndex = 0;
    
    // Start is called before the first frame update
    void Start()
    {
		StateManager sm = StateManager.GetController();
		PlayerData pd = sm.pdata;

        if (pd.Squads[SquadIndex] == null) {
            pd.Squads[SquadIndex] = new Squad4();
        }

        for (int i = 0; i <= 3; i++)
        {
            if (pd.Squads[SquadIndex].vectors[i] != null)
            {
                transform.Find("Grid/Slot " + i).GetComponent<VectorTile>().Populate(pd.Squads[SquadIndex].vectors[i]);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveSquad()
    {
        StateManager sm = StateManager.GetController();
        PlayerData pd = sm.pdata;

        for (int i = 0; i <= 3; i++)
        {
            pd.Squads[SquadIndex].vectors[i] = transform.Find("Grid/Slot " + i).GetComponent<VectorTile>().VLocal;
        }
        pd.Save();
    }
}
