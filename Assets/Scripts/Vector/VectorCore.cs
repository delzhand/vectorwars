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
}

[Serializable]
public class VectorCoreList
{
    public List<VectorCore> vector_core_list;
}