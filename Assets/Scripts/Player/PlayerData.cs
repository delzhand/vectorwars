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

    public static PlayerData DemoInit()
    {
        PlayerData pd = new PlayerData();
        pd.PlayerName = "Fukurou";
        pd.Squads = new Squad4[10];
        pd.Circuits = 1000;
        pd.Bits = 100;
        pd.Cores = 15;
        pd.VectorLocals = new VectorLocal[100];
        pd.VectorLocals[0] = new VectorLocal(0, 3);
        pd.VectorLocals[1] = new VectorLocal(1, 3);
        pd.VectorLocals[2] = new VectorLocal(2, 3);
        pd.VectorLocals[3] = new VectorLocal(3, 4);
        pd.VectorLocals[4] = new VectorLocal(4, 4);
        pd.VectorLocals[5] = new VectorLocal(5, 4);
        pd.VectorLocals[6] = new VectorLocal(6, 4);
        pd.VectorLocals[7] = new VectorLocal(7, 4);
        pd.Save();
        return pd;
    }

    public static PlayerData Load()
    {
        string json = PlayerPrefs.GetString("PlayerData", null);
        if (json.Length == 0)
        {
            return null;
        }
        PlayerData pd = JsonUtility.FromJson<PlayerData>(json);
        return pd;
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(this);
        PlayerPrefs.SetString("PlayerData", json);
    }

    public void Upload()
    {

    }

    public override string ToString()
    {
        return JsonUtility.ToJson(this);
    }
}
