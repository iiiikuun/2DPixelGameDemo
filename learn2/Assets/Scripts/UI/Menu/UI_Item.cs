using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Item : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    protected Image itemImage => GetComponent<Image>();
    protected CanvasGroup cg => GetComponent<CanvasGroup>();
    protected Canvas canvas => GetComponentInParent<Canvas>();
    protected UI_Menu ui => GetComponentInParent<UI_Menu>();

    public ItemType itemType;
    public Vector2Int itemPosition;

    public Vector2 defaultPosition;
    protected Transform originalParent;

    public bool isMoved;

    public virtual void SetPosition(Vector2Int _itemPosition)
    {
        itemPosition = _itemPosition;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        cg.blocksRaycasts = false;
        isMoved = false;

        defaultPosition = transform.position;
        originalParent = transform.parent;
        transform.SetParent(canvas.transform);

        transform.SetAsLastSibling();
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        cg.blocksRaycasts = true;

        if (!isMoved)
        {
            transform.SetParent(originalParent);
            transform.position = defaultPosition;
        }
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        UI_Item ui_Item = eventData.pointerDrag.GetComponent<UI_Item>();

        if (ui_Item.itemType == itemType)
        {
            if (itemType == ItemType.材料)
            {
                Inventory.instance.ExchangeMaterialPosition(ui_Item.itemPosition, itemPosition);
                ui_Item.isMoved = true;
            }
        }
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {

    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        ui.itemToolTip.HideToolTip();
    }

    public void DeleteUI() => Destroy(gameObject);
}
