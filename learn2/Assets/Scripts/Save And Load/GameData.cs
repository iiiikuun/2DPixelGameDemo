using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int currency;
    public int skillPoint;
    public int level;
    public int experience;

    public float SFX_Volume;
    public float BGM_Volume;

    public SerializableDictionary<string, bool> skillTree = new SerializableDictionary<string, bool>();

    public List<MaterialSaveData> materialInventory = new List<MaterialSaveData>();
    public List<EquipmentSaveData> equipmentInventory = new List<EquipmentSaveData>();


    public GameData()
    {
        currency = 0;
        skillPoint = 0;
        level = 0;
        experience = 0;
    }
}
