using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public abstract class Weapon : MonoBehaviour // �̹� ��븦 ��� ��
{
    // ����
    public float bulletDmg;
    public float bulletSpeed;

    // ����?
    protected Transform muzzle;



    // ��ƼŬ �ý��� - ������Ʈ �ܺδϱ� public����
    public ParticleSystem muzzleFlash;


    //���
    
    protected abstract void Fire(); // ���� ���� �� �ϰ� �ڽĵ��� ����
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
        // �Ѿ� �߻�
        // 0: ��Ŭ��, 1: ��Ŭ��, 2: ��, 3: �߰���ư ��, 4: �߰���ư ��
        if (isFullAuto && Input.GetMouseButton(0))
        {
            Fire();
        }
        else if (Input.GetMouseButtonDown(0)) 
        {
            Fire();
        }

        // Ÿ�̸� �ʱ�ȭ�ϴ� �Լ�
        if(isFullAuto && Input.GetMouseButtonUp(0)) OnRelease();
        
    }

    
}
