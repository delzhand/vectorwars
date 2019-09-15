using System;
using UnityEngine;

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
}
