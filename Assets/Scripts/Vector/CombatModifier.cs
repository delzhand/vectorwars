using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatBonusType
{
    flat,
    percent
}

public class CombatModifier : MonoBehaviour
{
    public CombatBonusType Type;
    public int Value;
    public string Stat;

    public int BonusValue(int baseValue)
    {
        if (Type == CombatBonusType.percent)
        {
            return Mathf.FloorToInt(baseValue * Value / 100f);
        }

        return Value;
    }
}
