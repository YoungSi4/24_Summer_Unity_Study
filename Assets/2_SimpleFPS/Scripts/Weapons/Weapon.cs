using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public abstract class Weapon : MonoBehaviour // 이미 모노를 상속 중
{
    // 스탯
    public float bulletDmg;
    public float bulletSpeed;

    // 구조?
    protected Transform muzzle;



    // 파티클 시스템 - 오브젝트 외부니까 public으로
    public ParticleSystem muzzleFlash;


    //상속
    
    protected abstract void Fire(); // 내가 구현 안 하고 자식들이 구현
    protected abstract void OnRelease();

    public void Init(Transform muzzlePos)
    {
        muzzle = muzzlePos;
    }

    void Start()
    {
        
    }


    public bool isFullAuto;
    void Update()
    {
        // 총알 발사
        // 0: 좌클릭, 1: 우클릭, 2: 휠, 3: 추가버튼 앞, 4: 추가버튼 뒤
        if (isFullAuto && Input.GetMouseButton(0))
        {
            Fire();
        }
        else if (Input.GetMouseButtonDown(0)) 
        {
            Fire();
        }

        // 타이머 초기화하는 함수
        if(isFullAuto && Input.GetMouseButtonUp(0)) OnRelease();
        
    }

    
}
