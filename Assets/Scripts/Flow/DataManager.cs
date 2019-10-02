using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static Dictionary<int, VectorCore> CoreLibrary = GetCoreLibrary();

    public static Dictionary<int, VectorCore> GetCoreLibrary()
    {
        Dictionary<int, VectorCore> library = new Dictionary<int, VectorCore>();
        VectorCoreList vcl = LoadCoreList();
        foreach(VectorCore vc in vcl.vectors)
        {
            if (library.ContainsKey(vc.id))
            {
                library[vc.id] = vc;
            }
            else
            {
                library.Add(vc.id, vc);
            }
        }
        return library;
    }

    public static void UpdateFromBundle(AssetBundle bundle)
    {
        Console.Log("Bundle " + bundle.name + " received.");
        VectorCoreList mainList = LoadCoreList();
        bool mainListChanged = false;
        string[] assetNames = bundle.GetAllAssetNames();
        foreach(string assetName in assetNames)
        {
            if (assetName.Contains("vectors.json"))
            {
                Console.Log("Bundle contains vectors.");
                TextAsset t = bundle.LoadAsset<TextAsset>(assetName);
                VectorCoreList toAppend = JsonUtility.FromJson<VectorCoreList>(t.text);
                foreach (VectorCore vc in toAppend.vectors)
                {
                    Console.Log("Vector '" + vc.name + "' added to list.");
                    mainList.vectors.Add(vc);
                    mainListChanged = true;
                }
            }
            if (assetName.Contains("_sprite.png"))
            {
                Texture2D t = bundle.LoadAsset<Texture2D>(assetName);
                string filename = Path.GetFileName(assetName);
                string path = Path.Combine(Application.persistentDataPath, "GameData/Sprites/" + filename);
                byte[] bytes = t.EncodeToPNG();
                File.WriteAllBytes(path, bytes);
                Console.Log("Sprite " + assetName + " saved.");
            }
        }
        if (mainListChanged)
        {
            SaveCoreList(mainList);
            Console.Log("Core list updated successfully.");
        }
        CoreLibrary = GetCoreLibrary();
    }

    private static VectorCoreList LoadCoreList()
    {
        string path = Path.Combine(Application.persistentDataPath, "GameData/CoreLibrary.json");
        if (File.Exists(path) == true)
        {
            string coreJson = File.ReadAllText(path);
            return JsonUtility.FromJson<VectorCoreList>(coreJson);
        }

        return new VectorCoreList();
    }

    private static void SaveCoreList(VectorCoreList list)
    {
        string path = Path.Combine(Application.persistentDataPath, "GameData/CoreLibrary.json");
        File.WriteAllText(path, JsonUtility.ToJson(list));
    }
}
