using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_ItemSlot : MonoBehaviour,IDropHandler
{
    protected ItemType itemType;

    protected Vector2Int slotPosition;

    protected virtual void Awake()
    {

    }

    public virtual void SetUpSlot(ItemType _itemType, Vector2Int _slotPosition)
    {
        itemType = _itemType;
        slotPosition = _slotPosition;
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.TryGetComponent(out UI_Item ui_Item) && ui_Item.itemType == itemType)
        {
            if (ui_Item.itemPosition == new Vector2Int(-1, -1) && eventData.pointerDrag.TryGetComponent(out UI_Equipment ui_Equipment))
            {
                Inventory.instance.ChangeEquipment(ui_Equipment.item, null, slotPosition);
                ui_Item.isMoved = true;
            }

            if (itemType == ItemType.材料)
            {
                Inventory.instance.ExchangeMaterialPosition(ui_Item.itemPosition, slotPosition);
                ui_Item.isMoved = true;
            }
            else if (itemType == ItemType.装备)
            {
                Inventory.instance.ExchangeEquipmentPosition(ui_Item.itemPosition, slotPosition);
                ui_Item.isMoved = true;
            }
        }
    }
}
