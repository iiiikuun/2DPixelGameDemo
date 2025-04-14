using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[System.Serializable]
public class Item_Material :Item
{
    public ItemData_Material data;
    public int stackSize;

    public UI_Material ui_Material;

    public System.Action stackSizeChangeed;

    public Item_Material (ItemData_Material _data,int count)
    {
        data = _data;
        AddStack(count);
    }

    public void AddStack(int amount)
    {
        stackSize += amount;

        if(stackSizeChangeed != null) 
            stackSizeChangeed();
    }
    public void RemoveStack(int amount)
    {
        stackSize -= amount;

        if (stackSizeChangeed != null)
            stackSizeChangeed();
    }

    public void DeleteUI() => ui_Material.DeleteUI();
}
