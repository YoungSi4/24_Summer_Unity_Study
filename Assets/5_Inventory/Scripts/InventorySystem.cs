using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    // 길이가 고정이므로 배열로 생성
    public Slot[] itemSlots;

    // 툴팁
    public GameObject tooltipObj;
    public RectTransform tooltipTransform;
    public TextMeshProUGUI tooltipItemName;
    public TextMeshProUGUI tooltipItemDesc;

    public RectTransform iconLayer;

    private ItemIcon icon;

    // 해시테이블로 아이템 데이터를 들고 있자 - 유니티에서 dict은 public으로도 노출이 안 된다.
    // 간접적인 방법으로 노출시켜야 한다.
    public Dictionary<string, ItemData> ItemDB;
    public ItemData[] itemDataGroup; // 시스템이 시작하면 여기서 dict으로 옮겨주자

    private void Start()
    {

        ItemDB = new Dictionary<string, ItemData>();
        foreach (var data in itemDataGroup)
        {
            ItemDB.Add(data.name, data);
        }


        // SetItem 3번 호출
        foreach (var itemKey in ItemDB.Keys)
        {
            SetItem(itemKey);
        }
    }

    public bool SetItem(string itemKey) // 성공적으로
    {
        Slot targetSlot = null;

        for(int i = 0; i < itemSlots.Length; i++)
        {
            if(!itemSlots[i].isFilled) { 
                targetSlot = itemSlots[i];
                break;
            }
        }

        // 아이템 슬롯이 꽉차서 못 넣을 때
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
        // anchoredPosition: UI의 Pivot을 기준으로 위치를 계산
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
