using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Rifle : Weapon
{
    public float fireRate;
    public LineRenderer line;
    // 연사 조정
    private float timer = 0f;


    protected override void Fire()
    {
        timer += Time.deltaTime;



        if(timer > fireRate)
        {

            muzzleFlash.Play();
            line.enabled = true; // 연사 중에 여러번 키는 게 아쉽다.

            

            // 발사
            var randomPos = Random.insideUnitCircle;
            var targetPos = new Vector3(randomPos.x, randomPos.y, 50f);
            line.SetPosition(1, targetPos);
            timer -= fireRate;


            // 히트스캔
            var bulletDir = muzzle.rotation * targetPos; // 로테이션은 quaternion
                                                   // rotation * vector >> vector만큼 roatation을 돌려준다?
            
            
            if (Physics.Raycast(muzzle.position, bulletDir, out var hit, 100, 1 << 7))
            {
                // 혹은 이런 방법도 있다: LayerMask.NameToLayer("Enemy");
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
