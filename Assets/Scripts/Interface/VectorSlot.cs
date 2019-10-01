using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorSlot : MonoBehaviour
{
    public int SquadIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static bool AnySlotsOpen()
    {
        foreach (VectorSlot vs in FindObjectsOfType<VectorSlot>())
        {
            if (vs.GetComponent<VectorTile>().VLocal == null)
            {
                return true;
            }
        }
        return false;
    }

    public static VectorSlot NextSlotFilled(bool reverseOrder = false)
    {
        VectorSlot[] slots = FindObjectsOfType<VectorSlot>();
        if (reverseOrder)
        {
            Array.Reverse(slots);
        }
        foreach (VectorSlot vs in slots)
        {
            if (vs.GetComponent<VectorTile>().VLocal != null)
            {
                return vs;
            }
        }
        return null;
    }

    public static VectorSlot NextSlotOpen(bool reverseOrder = false)
    {
        VectorSlot[] slots = FindObjectsOfType<VectorSlot>();
        Array.Reverse(slots);
        foreach (VectorSlot vs in slots)
        {
            if (vs.GetComponent<VectorTile>().VLocal == null)
            {
                return vs;
            }
        }
        return null;
    }

    public static bool isVectorAssignedAnySlot(VectorLocal vlocal)
    {
        foreach (VectorSlot vs in FindObjectsOfType<VectorSlot>())
        {
            if (vs.GetComponent<VectorTile>().VLocal == vlocal)
            {
                return true;
            }
        }
        return false;
    }

    public static VectorSlot whatSlotIsVectorAssigned(VectorLocal vlocal)
    {
        foreach (VectorSlot vs in FindObjectsOfType<VectorSlot>())
        {
            if (vs.GetComponent<VectorTile>().VLocal == vlocal)
            {
                return vs;
            }
        }
        return null;
    }
}
