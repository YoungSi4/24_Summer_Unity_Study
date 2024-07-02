using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    // ���̰� �����̹Ƿ� �迭�� ����
    public Slot[] itemSlots;

    // ����
    public GameObject tooltipObj;
    public RectTransform tooltipTransform;
    public TextMeshProUGUI tooltipItemName;
    public TextMeshProUGUI tooltipItemDesc;

    public RectTransform iconLayer;

    private ItemIcon icon;

    // �ؽ����̺�� ������ �����͸� ��� ���� - ����Ƽ���� dict�� public���ε� ������ �� �ȴ�.
    // �������� ������� ������Ѿ� �Ѵ�.
    public Dictionary<string, ItemData> ItemDB;
    public ItemData[] itemDataGroup; // �ý����� �����ϸ� ���⼭ dict���� �Ű�����

    private void Start()
    {

        ItemDB = new Dictionary<string, ItemData>();
        foreach (var data in itemDataGroup)
        {
            ItemDB.Add(data.name, data);
        }


        // SetItem 3�� ȣ��
        foreach (var itemKey in ItemDB.Keys)
        {
            SetItem(itemKey);
        }
    }

    public bool SetItem(string itemKey) // ����������
    {
        Slot targetSlot = null;

        for(int i = 0; i < itemSlots.Length; i++)
        {
            if(!itemSlots[i].isFilled) { 
                targetSlot = itemSlots[i];
                break;
            }
        }

        // ������ ������ ������ �� ���� ��
        if (targetSlot == null) return false;

        targetSlot.SetItem(ItemDB[itemKey]);

        return true;
    }


    public void InitTooltip(ItemData itemData)
    {
        tooltipObj.SetActive(true);

        tooltipItemName.text = itemData.itemName;
        tooltipItemDesc.text = itemData.itemDescripts;
    }

    public void DisableTooltip()
    {
        tooltipObj.SetActive(false);
    }

    public void TooltipMove(Vector2 pos)
    {
        // anchoredPosition: UI�� Pivot�� �������� ��ġ�� ���
        tooltipTransform.anchoredPosition = pos;
    }

    public void InitDrag(ItemIcon itemIcon)
    {
        icon = itemIcon;

        // contact to iconLayer gameObject
        icon.transform.SetParent(iconLayer); // move to iconLayer temporary.

        tooltipObj.SetActive(false);
        icon.GetComponent<Image>().raycastTarget = false;

        icon.SetPos();
    }
}
