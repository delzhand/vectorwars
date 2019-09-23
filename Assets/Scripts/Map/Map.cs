using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private TileType[,] map;
    public int Width = 8;
    public int Height = 8;

    // Start is called before the first frame update
    void Start()
    {
        generateMap();
        Camera.main.transform.position = new Vector3(3.5f, 3.5f, -10);
        Camera.main.orthographicSize = 7.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void generateMap() {
        map = new TileType[Width, Height];
        for (int j = 0; j < Height; j++) {
            GameObject row = new GameObject();
            row.name = "Row " + j;
            row.transform.SetParent(this.transform);
            for (int i = 0; i < Width; i++) {
                int r = Random.Range(0, 100);
                TileType t = TileType.standard;
                if (r < 20)
                {
                    t = TileType.blocked;
                }
                if (r < 5)
                {
                    t = TileType.electric;
                }
                map[i,j] = t;
                GameObject g = (GameObject)Instantiate(Resources.Load("Map/tile-" + map[i, j].ToString()), new Vector3(i, -j, 0), Quaternion.identity);
                g.name = "[" + i + ", " + j + "]";
                g.transform.SetParent(row.transform, true);

            }
        }
    }
}
