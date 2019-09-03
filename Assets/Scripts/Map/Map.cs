using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private TileType[,] map;

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
        map = new TileType[8,8];
        for (int j = 0; j < 8; j++) {
            for (int i = 0; i < 8; i++) {
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
                Instantiate(Resources.Load("Map/tile-" + map[i, j].ToString()), new Vector3(i, j, 0), Quaternion.identity);
            }
        }
    }
}
