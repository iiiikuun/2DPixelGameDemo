using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    武器,
    头盔,
    战甲,
    战靴,
    饰品,
    消耗品
}

[System.Serializable]
public struct StatRange
{
    public int min;
    public int max;
    public int upgradeBonus;
}

[CreateAssetMenu(fileName = "New Item Date", menuName = "Date/Equipment")]

public class ItemData_Equipment : ItemData
{
    public EquipmentType equipmentType;

    [Header("基础属性")]
    public StatRange strength;
    public StatRange agility;
    public StatRange intelligence;
    public StatRange vitality;

    [Header("攻击属性")]
    public StatRange damage;
    public StatRange attackSpeed;
    public StatRange magicPower;
    public StatRange critChance;
    public StatRange critPower;
    public StatRange damageIncrease;
    public StatRange armorPiercing;
    public StatRange magicPiercing;

    [Header("防御属性")]
    public StatRange maxHealth;
    public StatRange armor;
    public StatRange magicResistance;
    public StatRange evasion;
    public StatRange damageDecrease;
    public StatRange resilience;

    [Header("其他属性")]
    public StatRange maxMana;
    public StatRange moveSpeed;

    [Header("合成方式")]
    public List<Item_Material> craftingMatrrials;
}
