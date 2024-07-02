using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 이 스크립트 자체는 데이터 컨테이너처럼 사용

// 이게 뭔데...
// 태그를 달아주는 용도?
public enum ItemType
{
    Weapon, Armor, Consumable/*소모품*/
}


// 사용자 지정 에셋 메뉴 생성
[CreateAssetMenu(fileName = "New Item Data", menuName = "CustomData/Create Item Data")]
public class ItemData : ScriptableObject // 상속변경 - 스크립터블 오브젝트
{
    public Sprite itemImage;
    public string itemName;

    [TextArea]
    public string itemDescripts;
    public ItemType type;

    // 보조함수는 필요하면 그때 만들기

}
