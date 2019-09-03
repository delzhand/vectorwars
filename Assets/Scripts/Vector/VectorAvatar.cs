using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * VectorAvatar represents the on-screen state of a Vector during gameplay
 */
public class VectorAvatar : MonoBehaviour
{
    public int X;
    public int Y;
    public VectorCore VectorCore;

    public void Initialize(VectorCore core, int x, int y)
    {
        this.VectorCore = core;
        this.X = x;
        this.Y = y;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
