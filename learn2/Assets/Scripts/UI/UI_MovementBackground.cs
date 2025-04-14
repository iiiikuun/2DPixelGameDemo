using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MovementBackground : MonoBehaviour
{
    RectTransform rectTransform => GetComponent<RectTransform>();

    private float width;
    private float height;
    private float xDistanceToMpve;
    private float yDistanceToMpve;

    private void Awake()
    {
        width = rectTransform.rect.width; 
        height = rectTransform.rect.height;
        xDistanceToMpve = width - Screen.width;
        yDistanceToMpve= height - Screen.height;
    }

    private void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        mousePosition.x = Mathf.Clamp(mousePosition.x, 0, Screen.width);
        mousePosition.y = Mathf.Clamp(mousePosition.y, 0, Screen.height);

        transform.position = new Vector2(-mousePosition.x * (xDistanceToMpve / Screen.width), -mousePosition.y * (yDistanceToMpve / Screen.height));
        //轴心和锚点均位于左下角
    }
}
