using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    // ���ʿ� ��������� ��
    public InventorySystem InventorySystem;
    private ItemData itemData; // ���⼭�� ������ �־�� ��

    // �̹����� �޾Ƽ� ĭ�� ��ġ��Ű�� @ ���� �����ϴ� ��
    public Image itemImage;

    private bool _isFilled = false; // ���������� ���
    public bool isFilled => _isFilled; // �ܺ� �б� ���� property

    public void SetItem(ItemData data) // ��� - ������ ���� �ʿ�
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
        // (���׿� �� �ΰ��ϰ�): if�� ó�� X
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
