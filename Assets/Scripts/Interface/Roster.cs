using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roster : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerData pd = StateManager.GetController().pdata;
        foreach (VectorLocal vl in pd.VectorLocals)
        {
            VectorTile.Create(vl, Vector2.zero, transform);
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
