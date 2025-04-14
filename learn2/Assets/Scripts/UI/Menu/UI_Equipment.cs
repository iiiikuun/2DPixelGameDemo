using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Equipment : UI_Item
{
    public Item_Equipment item;

    public void SetUpItem(Item_Equipment _Item, Vector2Int _itemPosition)
    {
        base.SetPosition(_itemPosition);

        item = _Item;

        itemType = ItemType.装备;
        itemImage.sprite = item.data.icon;
    }

    public override void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.TryGetComponent(out UI_Equipment ui_Equipment))
        {
            if (itemPosition != new Vector2Int(-1, -1) && ui_Equipment.itemPosition != new Vector2Int(-1, -1))
            {
                Inventory.instance.ExchangeEquipmentPosition(ui_Equipment.itemPosition, itemPosition);
                ui_Equipment.isMoved = true;
            }
            else if (item.data.equipmentType == ui_Equipment.item.data.equipmentType && itemPosition == new Vector2Int(-1, -1) && ui_Equipment.itemPosition != new Vector2Int(-1, -1))
            {
                Inventory.instance.ChangeEquipment(item, ui_Equipment.item, ui_Equipment.itemPosition);
                ui_Equipment.isMoved = true;
            }
            else if (item.data.equipmentType == ui_Equipment.item.data.equipmentType && ui_Equipment.itemPosition == new Vector2Int(-1, -1) && itemPosition != new Vector2Int(-1, -1))
            {
                Inventory.instance.ChangeEquipment(ui_Equipment.item, item, itemPosition);
                ui_Equipment.isMoved = true;
            }
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        ui.itemToolTip.ShowToolTip(item);
    }
}
