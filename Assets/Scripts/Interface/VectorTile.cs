using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public enum VectorTileDisplayMode
{
    name,
    level,
    rank
}

public class VectorTile : MonoBehaviour
{
    public VectorLocal V;
    public VectorTileDisplayMode mode = VectorTileDisplayMode.name;

    // Start is called before the first frame update
    void Start()
    {
        transform.Find("Image").GetComponent<Image>().sprite = getSprite();
        Text t = transform.Find("Bottom Text").GetComponent<Text>();
        switch (mode)
        {
            case VectorTileDisplayMode.name:
                t.text = DataManager.CoreLibrary[V.Core].name;
                break;
            case VectorTileDisplayMode.level:
                t.text = "Lv " + V.Level;
                break;
            case VectorTileDisplayMode.rank:
                t.text = "Rank " + V.Rank;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Sprite getSprite()
    {
        string path = Path.Combine(Application.persistentDataPath, "GameData/Sprites/" + DataManager.CoreLibrary[V.Core].name + "_sprite.png");
        if (File.Exists(path) == true)
        {
            byte[] bytes = File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(4, 4, TextureFormat.RGBA32, false);
            texture.filterMode = FilterMode.Point;
            texture.LoadImage(bytes);
            Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 32);
            return sp;
        }
        return null;
    }

    public static GameObject Create(VectorLocal v, Vector2 position, Transform parent)
    {
        GameObject g = (GameObject)Instantiate(Resources.Load("Interface/Vector Tile"), new Vector3(position.x, position.y, 0), Quaternion.identity);
        g.GetComponent<VectorTile>().V = v;
        g.transform.SetParent(parent, false);
        return g;
    }

}
