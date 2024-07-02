using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �� ��ũ��Ʈ ��ü�� ������ �����̳�ó�� ���

// �̰� ����...
// �±׸� �޾��ִ� �뵵?
public enum ItemType
{
    Weapon, Armor, Consumable/*�Ҹ�ǰ*/
}


// ����� ���� ���� �޴� ����
[CreateAssetMenu(fileName = "New Item Data", menuName = "CustomData/Create Item Data")]
public class ItemData : ScriptableObject // ��Ӻ��� - ��ũ���ͺ� ������Ʈ
{
    public Sprite itemImage;
    public string itemName;

    [TextArea]
    public string itemDescripts;
    public ItemType type;

    // �����Լ��� �ʿ��ϸ� �׶� �����

}
