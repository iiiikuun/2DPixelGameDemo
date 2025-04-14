using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_SkillToolTip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillDescription;

    public RectTransform rectTransform => GetComponent<RectTransform>();

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

    public void ShowToolTip(string _skillName,string _skillDescription)
    {
        skillName.text =  _skillName;
        skillDescription.text = _skillDescription;

        gameObject.SetActive(true);
    }

    public void HideToolTip()
    {
        rectTransform.position = new Vector2(-1000, 0);
        gameObject.SetActive(false);
    }
}
