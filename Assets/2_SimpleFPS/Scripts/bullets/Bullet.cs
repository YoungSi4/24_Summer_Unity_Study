using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // ����
    private float bulletSpeed; // �ѱ⳪ ĳ������ ������ �ٸ� �� �����Ƿ� �ʱ�ȭ�ؼ� �Ѱ��ֱ⸸ ����
    private float bulletDamage;

    // ����
    private bool isInit = false;
    private Vector3 bulletVector = Vector3.forward;

    // �Ҹ�
    private float timeCounter = 0f;
    private const float bulletMaxTime = 5f;

    // �ǰ� �� vfx
    public GameObject hitVFX;

    // ����Ƽ�� ������ ����� �������� �ʴ´�.
    // public���� �� �ʱ�ȭ �Լ��� ���� ����� ����ϴ� �� ����.


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
        Destroy(gameObject); // gameObject: �����. �� ������Ʈ�� �޷��ִ� ���� ������Ʈ�� ����.

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

    private void OnTriggerEnter(Collider other) // bullet �����տ��� trigger�� ���ְ� �;���
    {
        // ������ �¾��� ��
        if(other.gameObject.layer == 7)
        {
            // ���� ������ �ִ� �������� �޴� �Լ��� ȣ��
            other.GetComponent<Enemy>().GetDamage(bulletDamage);
            
            Instantiate(hitVFX, transform.position, Quaternion.identity);
            

            Destroy(gameObject);


        }
    }
}
