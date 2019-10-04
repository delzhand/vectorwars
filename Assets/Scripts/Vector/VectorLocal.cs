using System;
using UnityEngine;

[Serializable]
public class VectorLocal
{
    public int Rank;
    public int Level;
    public int XP;
    public int Core;
    public DateTime Acquired;

    public VectorLocal(int core, int rank)
    {
        this.Core = core;
        this.Rank = rank;
        this.Level = 1;
        this.XP = 0;
        this.Acquired = DateTime.UtcNow;
    }

    public override string ToString()
    {
        return DataManager.CoreLibrary[Core].name + " | Level " + Level + " | Rank " + Rank;
    }

    public static implicit operator bool (VectorLocal v)
    {
        return v.Rank > 0;
    }
}
