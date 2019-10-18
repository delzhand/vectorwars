using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Tile : MonoBehaviour
{
    public int X;
    public int Y;

    public Vector3 GetTop()
    {
        Vector3 p = transform.position;
        RaycastHit r;
        Physics.Raycast(transform.position + new Vector3(0, 100, 0), Vector3.down, out r, 100);
        p.y = r.point.y;
        return p;
    }
}
