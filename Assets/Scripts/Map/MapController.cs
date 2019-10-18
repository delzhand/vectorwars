using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    //private TileType[,] map;
    public int Width = 8;
    public int Height = 8;
    public bool Generate = false;

    // Start is called before the first frame update
    void Start()
    {
        if (Generate)
        {
            generateMap();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void generateMap() {
        Object light = Resources.Load("Map/Tile 1 Light");
        Object dark = Resources.Load("Map/Tile 1 Dark");
        for (int j = 0; j < Height; j++) {
            GameObject row = new GameObject();
            row.name = "Row " + j;
            row.transform.SetParent(this.transform);
            for (int i = 0; i < Width; i++) {
                GameObject g = (GameObject)Instantiate(((i+j)%2 == 1) ? light : dark, new Vector3(-i, 0, j), Quaternion.identity);
                Tile t = g.GetComponent<Tile>();
                t.X = i;
                t.Y = j;
                g.name = "[" + i + ", " + j + "]";
                g.transform.SetParent(row.transform, true);

            }
        }
    }
}
