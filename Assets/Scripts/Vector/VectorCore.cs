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
    public string sprite;

    public static Dictionary<int, VectorCore> GetLibrary() {
        Dictionary<int, VectorCore> library = new Dictionary<int, VectorCore>();
        dev_generate_vector("Zone", 0, library);
        dev_generate_vector("Primavera", 1, library);
        dev_generate_vector("Kepler", 2, library);
        dev_generate_vector("Strix", 3, library);
        dev_generate_vector("01ivia", 4, library);
        dev_generate_vector("Ganymede", 5, library);
        return library;
    }

    private static void dev_generate_vector(string name, int id, Dictionary<int, VectorCore> library)
    {   
        VectorCore vc = new VectorCore();
        vc.name = name;
        vc.id = id;
        library.Add(id, vc);
    }
}

[Serializable]
public class VectorCoreList
{
    public List<VectorCore> vector_core_list;
}