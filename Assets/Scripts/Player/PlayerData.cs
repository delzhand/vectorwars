using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public string PlayerName;
    public int Circuits;
    public int Bits;
    public int Cores;
    public List<KeyValuePair<int, int>> Skills;
    public List<VectorLocal> VectorLocals;
    public Squad4[] Squads;

    public void DemoInit()
    {
        PlayerName = "Fukurou";
        Squads = new Squad4[10];
        Circuits = 1000;
        Bits = 100;
        Cores = 15;
        Skills = new List<KeyValuePair<int, int>>();
        Skills.Add(new KeyValuePair<int, int>(1, 5));
        Skills.Add(new KeyValuePair<int, int>(2, 5));
        Skills.Add(new KeyValuePair<int, int>(3, 5));
        Skills.Add(new KeyValuePair<int, int>(4, 5));
        Skills.Add(new KeyValuePair<int, int>(5, 5));
        VectorLocals = new List<VectorLocal>();
        VectorLocals.Add(new VectorLocal(0, 3));
        VectorLocals.Add(new VectorLocal(1, 3));
        VectorLocals.Add(new VectorLocal(2, 3));
        VectorLocals.Add(new VectorLocal(3, 4));
        VectorLocals.Add(new VectorLocal(4, 4));
        VectorLocals.Add(new VectorLocal(5, 4));
        VectorLocals.Add(new VectorLocal(6, 4));
        VectorLocals.Add(new VectorLocal(7, 4));
    }
}
