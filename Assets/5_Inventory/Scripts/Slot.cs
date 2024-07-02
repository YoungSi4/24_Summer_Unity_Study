using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    // 위쪽에 전달해줘야 함
    public InventorySystem InventorySystem;
    private ItemData itemData; // 여기서도 가지고 있어야 함

    // 이미지를 받아서 칸에 위치시키고 @ 값을 조정하는 식
    public Image itemImage;

    private bool _isFilled = false; // 내부적으로 사용
    public bool isFilled => _isFilled; // 외부 읽기 전용 property

    public void SetItem(ItemData data) // 깃발 - 데이터 정의 필요
    {
        itemData = data;
        itemImage.sprite = data.itemImage;
        var tempColor = itemImage.color;
        tempColor.a = 225f;
        itemImage.color = tempColor;
        _isFilled = true;
    }
    
    public void MouseEnter()
    {
        if (itemData == null) return;

        InventorySystem.InitTooltip(itemData);

    }

    public void MouseExit()
    {
        // (버그에 덜 민감하게): if문 처리 X
        InventorySystem.DisableTooltip();
    }

    public void MouseMove(Vector2 pos)
    {
        InventorySystem.TooltipMove(pos);
    }

    public void MouseDown(ItemIcon icon)
    {
        InventorySystem.InitDrag(icon);
    }
}
