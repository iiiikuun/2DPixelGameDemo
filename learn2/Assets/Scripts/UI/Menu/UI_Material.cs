using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Material : UI_Item
{
    private TextMeshProUGUI itemText => GetComponentInChildren<TextMeshProUGUI>();

    public Item_Material item;

    public void SetUpItem(Item_Material _Item, Vector2Int _itemPosition)
    {
        base.SetPosition(_itemPosition);

        item = _Item;

        itemType = ItemType.材料;
        itemImage.sprite = item.data.icon;

        UpdateMaterial();

        item.stackSizeChangeed += UpdateMaterial;
    }

    public void UpdateMaterial()
    {
        if (item.stackSize > 1)
            itemText.text = item.stackSize.ToString();
        else
            itemText.text = null;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        ui.itemToolTip.ShowToolTip(item);
    }
}
