using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorSlot : MonoBehaviour
{
    public VectorTile SlotTile; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsOpen()
    {
        return SlotTile == null;
    }

    public void Assign(VectorTile source)
    {
        SlotTile = VectorTile.Create(source.VLocal, Vector2.zero, transform).GetComponent<VectorTile>();
    }

    public void Unassign()
    {

    }
}
