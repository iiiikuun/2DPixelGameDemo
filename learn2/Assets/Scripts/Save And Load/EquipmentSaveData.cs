using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipmentSaveData : ItemSaveData
{
    public int strength = 0; 
    public int agility = 0; 
    public int intelligence = 0; 
    public int vitality = 0; 

    public int damage = 0;
    public int attackSpeed = 0;
    public int magicPower = 0;
    public int critChance = 0;
    public int critPower = 0;
    public int damageIncrease = 0;
    public int armorPiercing = 0;
    public int magicPiercing = 0;

    public int maxHealth = 0;
    public int armor = 0;
    public int magicResistance = 0;
    public int evasion = 0;
    public int damageDecrease = 0;
    public int resilience = 0;

    public int maxMana = 0;
    public int moveSpeed = 0;

    public EquipmentSaveData()
    {
    }

    public EquipmentSaveData(Item_Equipment equipment)
    {
        itemID = equipment.data.itemID;

        strength = equipment.strength.GetValue();
        agility = equipment.agility.GetValue();
        intelligence = equipment.intelligence.GetValue();
        vitality = equipment.vitality.GetValue();

        damage = equipment.damage.GetValue();
        attackSpeed = equipment.attackSpeed.GetValue();
        magicPower = equipment.magicPower.GetValue();
        critChance = equipment.critChance.GetValue();
        critPower = equipment.critPower.GetValue();
        damageIncrease = equipment.damageIncrease.GetValue();
        armorPiercing = equipment.armorPiercing.GetValue();
        magicPiercing = equipment.magicPiercing.GetValue();

        maxHealth = equipment.maxHealth.GetValue();
        armor = equipment.armor.GetValue();
        magicResistance = equipment.magicResistance.GetValue();
        evasion = equipment.evasion.GetValue();
        damageDecrease = equipment.damageDecrease.GetValue();
        resilience = equipment.resilience.GetValue();

        maxMana = equipment.maxMana.GetValue();
        moveSpeed = equipment.moveSpeed.GetValue();
    }
}
