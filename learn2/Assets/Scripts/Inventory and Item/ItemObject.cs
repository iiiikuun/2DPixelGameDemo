using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    private Item item;

    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    private SpriteRenderer sr => GetComponent<SpriteRenderer>();
    [SerializeField] private Collider2D cd;

    public void PickupItem()
    {
        if (item is Item_Material)
        {
            Item_Material material = item as Item_Material;
            if (!Inventory.instance.IsMaterialInventoryFull(material.data))
            {
                Inventory.instance.AddMaterial(material);
                Destroy(gameObject);
            }
        }
        else if (item is Item_Equipment && !Inventory.instance.IsEquipmentInventoryFull())
        {
            Inventory.instance.AddEquipment(item as Item_Equipment);
            Destroy(gameObject);
        }
    }

    public void SetupItem(Item _item)
    {
        item = _item;

        cd.enabled = false;

        Invoke("CD_Enabled", 0.5f);

        if(item is Item_Material)
            sr.sprite = (item as Item_Material).data.icon;
        else if(item is Item_Equipment)
            sr.sprite = (item as Item_Equipment).data.icon;

        rb.velocity = new Vector2(Random.Range(-5f, 5f), Random.Range(10f, 20f));
    }

    private void CD_Enabled()
    {
        cd.enabled = true;
    }
}
