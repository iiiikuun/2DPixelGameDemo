using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemToolTip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemTypeText;
    [SerializeField] private TextMeshProUGUI itemQualityText;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI itemBonusText;
    private Image image=>GetComponent<Image>();
    public RectTransform rectTransform=>GetComponent<RectTransform>();

    private Vector2 rectSize;

    private void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        rectSize = rectTransform.rect.size;
        Vector2 mousePos = Input.mousePosition;

        Vector2 targetPos = new Vector2(
            mousePos.x + (mousePos.x < Screen.width / 2f ? 15 : -rectSize.x - 5), // 左右判断
            mousePos.y + (mousePos.y < Screen.height / 2f ? rectSize.y + 5 : -15) // 上下判断
        );

        rectTransform.position = targetPos;
    }

    public void ShowToolTip(Item item)
    {
        Vector3Int itemColorVector3;

        if (item is Item_Equipment)
        {
            Item_Equipment equipment = item as Item_Equipment;

            itemNameText.text = equipment.data.itemName;
            itemTypeText.text = equipment.data.equipmentType.ToString();
            itemQualityText.text = equipment.data.qualityType.ToString();
            itemDescription.text = equipment.data.itemDescription;
            itemBonusText.text = equipment.sb.ToString();

            itemColorVector3 = CheckColor(equipment.data.qualityType);
        }
        else
        {
            Item_Material material = item as Item_Material;

            itemNameText.text = material.data.itemName;
            itemTypeText.text = material.data.itemType.ToString();
            itemQualityText.text = material.data.qualityType.ToString();
            itemDescription.text = material.data.itemDescription;
            itemBonusText.text = "";

            itemColorVector3 = CheckColor(material.data.qualityType);
        }

        Color itemColor = new Color(itemColorVector3.x / 255f, itemColorVector3.y / 255f, itemColorVector3.z / 255f);
        itemNameText.color = itemColor;
        itemQualityText.color = itemColor;
        image.color = itemColor;

        gameObject.SetActive(true);
    }

    private Vector3Int CheckColor(QualityType qualityType) => qualityType switch
    {
        QualityType.普通 => new Vector3Int(255, 255, 255),
        QualityType.精良 => new Vector3Int(80, 150, 255),
        QualityType.史诗 => new Vector3Int(160, 50, 220),
        QualityType.传说 => new Vector3Int(255, 215, 0),
        QualityType.神话 => new Vector3Int(255, 50, 50),
        _ => new Vector3Int(255, 255, 255)
    };

    public void HideToolTip()
    {
        rectTransform.position = new Vector2(-1000, 0);
        gameObject.SetActive(false);
    }
}
