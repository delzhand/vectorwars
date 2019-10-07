using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VectorBehavior
{
    player,
    cpu
}

public class VectorCombat : MonoBehaviour
{
    public VectorBehavior Behavior;

    // Base Values
    public int BaseHP;
    public int BasePow;
    public int BaseMag;
    public int BaseCon;
    public int BaseRes;
    public int BaseSpd;
    public int BaseMov;

    // Combat Resources
    public int HP;
    public int CT;

    // Representation
    public string Name;
    private VectorAvatar avatar;

    public void init(VectorLocal local, VectorBehavior behavior)
    {
        VectorCore core = DataManager.CoreLibrary[local.Core];
        this.Name = core.name;
        this.BaseHP = core.hp;
        this.BasePow = core.pow;
        this.BaseMag = core.mag;
        this.BaseCon = core.con;
        this.BaseRes = core.res;
        this.BaseSpd = core.spd;
        this.BaseMov = core.mov;
        this.Behavior = behavior;
    }

    public void init(string name, int hp, int pow, int mag, int con, int res, int spd, int mov, VectorBehavior behavior)
    {
        this.name = name;
        this.BaseHP = hp;
        this.BasePow = pow;
        this.BaseMag = mag;
        this.BaseCon = con;
        this.BaseRes = res;
        this.BaseSpd = spd;
        this.BaseMov = mov;
        this.Behavior = behavior;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.HP = this.BaseHP;
        this.CT = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
