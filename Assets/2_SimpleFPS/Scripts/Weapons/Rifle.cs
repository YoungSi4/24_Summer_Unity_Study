using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Rifle : Weapon
{
    public float fireRate;
    public LineRenderer line;
    // ���� ����
    private float timer = 0f;


    protected override void Fire()
    {
        timer += Time.deltaTime;



        if(timer > fireRate)
        {

            muzzleFlash.Play();
            line.enabled = true; // ���� �߿� ������ Ű�� �� �ƽ���.

            

            // �߻�
            var randomPos = Random.insideUnitCircle;
            var targetPos = new Vector3(randomPos.x, randomPos.y, 50f);
            line.SetPosition(1, targetPos);
            timer -= fireRate;


            // ��Ʈ��ĵ
            var bulletDir = muzzle.rotation * targetPos; // �����̼��� quaternion
                                                   // rotation * vector >> vector��ŭ roatation�� �����ش�?
            
            
            if (Physics.Raycast(muzzle.position, bulletDir, out var hit, 100, 1 << 7))
            {
                // Ȥ�� �̷� ����� �ִ�: LayerMask.NameToLayer("Enemy");
                hit.transform.GetComponent<Enemy>().GetDamage(bulletDmg);
                
            }

            
        }
    }

    protected override void OnRelease()
    {
        timer = 0f;
        line.enabled = false;
    }
}
