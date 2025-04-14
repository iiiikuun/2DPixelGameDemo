using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MaterialSaveData : ItemSaveData
{
    public int stackSize = 0;

    public MaterialSaveData()
    {
    }

    public MaterialSaveData(Item_Material material)
    {
        itemID = material.data.itemID;
        stackSize = material.stackSize;
    }
}
