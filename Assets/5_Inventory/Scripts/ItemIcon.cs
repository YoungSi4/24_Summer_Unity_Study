using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 인터페이스 관련은 다중 상속 가능?
public class ItemIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler, IPointerDownHandler, IPointerUpHandler
{
    public Slot slot; // 어떤 슬롯 위에 있는지
    
    public Vector2 pos;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetPos()
    {
        rectTransform.anchoredPosition = pos;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        slot.MouseEnter(); // 어떤 슬롯 위로 마우스가 올라감
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        slot.MouseExit();
    }

    // 아이콘 상에 올라가 있는 동안
    public void OnPointerMove(PointerEventData eventData)
    {
        // 툴팁이 계속 따라옴
        slot.MouseMove(eventData.position);

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        slot.MouseDown(this);
        pos = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
