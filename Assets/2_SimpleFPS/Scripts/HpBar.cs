using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    public RectTransform rect;
    private float max = 96f;

    public void SetHpBar(float ratio) // ratio: 0 ~ 1
    {
        var size = rect.sizeDelta; // 2���� ����
        size.x = max * ratio; 
        rect.sizeDelta = size;
    }

    public void SetPosition(Vector3 pos)
    {
        var cam = Camera.main;
        // ī�޶� ���� �ٶ󺸴� ���� ���� ������ pos - cam ��ġ �� ���� 90���� ������ (=�þ� ���̸�)

        // �ڵ� ���ƴ�...
        bool active = (Vector3.Angle(cam.transform.forward, pos - cam.transform.position) < 90f);
        
        gameObject.SetActive(active);

        if (!active) return;
     
        var uiPos = Camera.main.WorldToScreenPoint(pos); // 3D -> 2D
        transform.position = uiPos;
    }
}
