using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorTileGrid : MonoBehaviour
{
    public int Columns;
    public int Gutter;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitGrid()
    {
        Vector2 size = GetComponent<RectTransform>().sizeDelta;
        float width = size.x;
        float totalGutterWidth = Gutter * (Columns - 1);
        float tileSize = (width - totalGutterWidth) / Columns;
        for (int i = 0; i < transform.childCount; i++)
        {
            Vector2 pos = new Vector2(i % Columns, -i / Columns);
            pos *= (tileSize + Gutter);
            pos -= size / 2;
            pos += new Vector2(tileSize, tileSize) / 2;
            pos += new Vector2(0, size.y - tileSize);
            transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition = pos;
            transform.GetChild(i).GetComponent<RectTransform>().sizeDelta = new Vector2(tileSize, tileSize);
        }
    }
}
