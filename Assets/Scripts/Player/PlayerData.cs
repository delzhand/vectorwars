using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public string PlayerName;
    public int Circuits;
    public int Bits;
    public int Cores;
    public VectorLocal[] VectorLocals;
    public Squad4[] Squads;

    public void DemoInit()
    {
        PlayerName = "Fukurou";
        Squads = new Squad4[10];
        Circuits = 1000;
        Bits = 100;
        Cores = 15;
        VectorLocals = new VectorLocal[100];
        VectorLocals[0] = new VectorLocal(0, 3);
        VectorLocals[1] = new VectorLocal(1, 3);
        VectorLocals[2] = new VectorLocal(2, 3);
        VectorLocals[3] = new VectorLocal(3, 4);
        VectorLocals[4] = new VectorLocal(4, 4);
        VectorLocals[5] = new VectorLocal(5, 4);
        VectorLocals[6] = new VectorLocal(6, 4);
        VectorLocals[7] = new VectorLocal(7, 4);
    }

    public static PlayerData Load()
    {
        string json = PlayerPrefs.GetString("PlayerData", null);
        if (json != null)
        {
            return JsonUtility.FromJson<PlayerData>(json);
        }
        return null;
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(this);
        PlayerPrefs.SetString("PlayerData", json);
    }

    public void Upload()
    {

    }
}
