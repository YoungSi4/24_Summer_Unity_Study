using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    public RectTransform rect;
    private float max = 96f;

    public void SetHpBar(float ratio) // ratio: 0 ~ 1
    {
        var size = rect.sizeDelta; // 2차원 벡터
        size.x = max * ratio; 
        rect.sizeDelta = size;
    }

    public void SetPosition(Vector3 pos)
    {
        var cam = Camera.main;
        // 카메라 정면 바라보는 각과 적이 생성된 pos - cam 위치 인 각이 90도를 넘으면 (=시야 밖이면)

        // 코드 미쳤누...
        bool active = (Vector3.Angle(cam.transform.forward, pos - cam.transform.position) < 90f);
        
        gameObject.SetActive(active);

        if (!active) return;
     
        var uiPos = Camera.main.WorldToScreenPoint(pos); // 3D -> 2D
        transform.position = uiPos;
    }
}
