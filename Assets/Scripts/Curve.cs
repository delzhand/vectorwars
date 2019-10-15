using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curve
{
    public static float Sin(float f)
    {
        return Mathf.Sin(Mathf.PI / 2 * f);
    }

    public static float CosInverse(float f)
    {
        return 1 - Mathf.Cos(Mathf.PI / 2 * f);
    }
}
