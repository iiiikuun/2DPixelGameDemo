using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EquipmentSlot : UI_ItemSlot
{
    public EquipmentType equipmentType;

    protected override void Awake()
    {
        base.Awake();

        itemType = ItemType.装备;
    }

    public override void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.TryGetComponent(out UI_Equipment ui_Equipment) && ui_Equipment.itemPosition != new Vector2Int(-1, -1) && ui_Equipment.item.data.equipmentType == equipmentType)
        {
            Inventory.instance.ChangeEquipment(null, ui_Equipment.item, ui_Equipment.itemPosition);
            ui_Equipment.isMoved = true;
        }
    }

    private void OnValidate()
    {
        gameObject.name = "Equipment slot - " + equipmentType.ToString();
    }
}


