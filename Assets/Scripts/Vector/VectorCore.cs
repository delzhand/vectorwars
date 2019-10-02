/*
 * VectorCore contains those values that never change across character growth,
 * IVs, or client
 */

using System;
using System.Collections.Generic;

[Serializable]
public class VectorCore
{
    public int id;
    public string name;
    public int mem;
    public int con;
    public int res;
    public int pow;
    public int mag;
    public int spd;
    public int mov;
    //public string sprite;

    //public static Dictionary<int, VectorCore> GetLibrary() {
    //    Dictionary<int, VectorCore> library = new Dictionary<int, VectorCore>();
    //    dev_generate_vector("X", 0, library);
    //    dev_generate_vector("Samus", 1, library);
    //    dev_generate_vector("Gurrenn", 2, library);
    //    dev_generate_vector("Hound", 3, library);
    //    dev_generate_vector("Alice", 4, library);
    //    dev_generate_vector("Aigis", 5, library);
    //    dev_generate_vector("Optimus", 6, library);
    //    dev_generate_vector("Cat", 7, library);
    //    return library;
    //}

    //private static void dev_generate_vector(string name, int id, Dictionary<int, VectorCore> library)
    //{   
    //    VectorCore vc = new VectorCore();
    //    vc.name = name;
    //    vc.id = id;
    //    library.Add(id, vc);
    //}

    public VectorCore(string name, int id)
    {
        this.name = name;
        this.id = id;
    }
}

[Serializable]
public class VectorCoreList
{
    public List<VectorCore> vectors;

    public VectorCoreList()
    {
        vectors = new List<VectorCore>();
    }
}