using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roster : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerData pd = StateManager.GetController().pdata;
        for (int i = 0; i < pd.VectorLocals.Length; i++)
        {
            if (pd.VectorLocals[i])
            {
                VectorTile.Create(pd.VectorLocals[i], Vector2.zero, transform);
            }
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
