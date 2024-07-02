using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 스탯
    private float bulletSpeed; // 총기나 캐릭마다 스탯이 다를 수 있으므로 초기화해서 넘겨주기만 하자
    private float bulletDamage;

    // 방향
    private bool isInit = false;
    private Vector3 bulletVector = Vector3.forward;

    // 소멸
    private float timeCounter = 0f;
    private const float bulletMaxTime = 5f;

    // 피격 시 vfx
    public GameObject hitVFX;

    // 유니티는 생성자 사용을 권장하지 않는다.
    // public으로 된 초기화 함수를 따로 만들어 사용하는 게 좋다.


    public void Init(float speed, float dmg)
    {
        bulletDamage = dmg;
        bulletSpeed = speed;

        isInit = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isInit) return; // early return?
        
        Debug.LogError("Bullet is not Initiated!");
        Destroy(gameObject); // gameObject: 예약어. 이 컴포넌트가 달려있는 게임 오브젝트를 제거.

        // if (!isInit) return;
        //
        //Debug.LogError("Bullet is not Initiated!");
        //Destroy(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(bulletVector * (bulletSpeed * Time.deltaTime));

        timeCounter += Time.deltaTime;
        if (timeCounter >= bulletMaxTime)
        {
            Destroy(gameObject);
        }
        

    }

    private void OnTriggerEnter(Collider other) // bullet 프리팹에서 trigger를 켜주고 와야함
    {
        // 적에게 맞았을 때
        if(other.gameObject.layer == 7)
        {
            // 적이 가지고 있는 데미지를 받는 함수를 호출
            other.GetComponent<Enemy>().GetDamage(bulletDamage);
            
            Instantiate(hitVFX, transform.position, Quaternion.identity);
            

            Destroy(gameObject);


        }
    }
}
