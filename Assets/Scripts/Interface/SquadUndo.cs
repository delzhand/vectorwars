using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadUndo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        if (VectorTile.selected)
        {
            if (VectorTile.selected.GetComponent<VectorSlot>())
            {
                VectorTile.selected.Depopulate();
                VectorTile.selected.Deselect();
            }
            else
            {
                VectorTile.selected.Deselect();
            }
        }
        else
        {
            if (VectorSlot.NextSlotFilled())
            {
                VectorSlot.NextSlotFilled().GetComponent<VectorTile>().Depopulate();
            }
        }
    }
}
