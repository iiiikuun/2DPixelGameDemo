using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private GameObject dropPrefab;

    public void DropItem(ItemData data)
    {
        Item_Material material;
        Item_Equipment equipment;

        GameObject newDrop = Instantiate(dropPrefab, transform.position, Quaternion.identity);

        if (data.itemType == ItemType.材料)
        {
            material = new Item_Material(data as ItemData_Material, 1);

            newDrop.GetComponent<ItemObject>().SetupItem(material);
        }
        else if (data.itemType == ItemType.装备)
        {
            equipment = new Item_Equipment(data as ItemData_Equipment);

            newDrop.GetComponent<ItemObject>().SetupItem(equipment);
        }
    }
}
