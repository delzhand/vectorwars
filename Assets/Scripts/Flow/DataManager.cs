using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static Dictionary<int, VectorCore> CoreLibrary = VectorCore.GetLibrary();
}
