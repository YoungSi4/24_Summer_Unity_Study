using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour
{
    // ����
    private float granadeSpeed;
    private float granadeDamage;
    private float granadeExplodeRange;

    // ����
    private bool isInit = false;
    private Vector3 granadeVector = Vector3.forward;

    // �Ҹ�
    private float timeCounter = 0f;
    private const float granadeMaxTime = 5f;

    // �ǰ� �� vfx
    public GameObject explosion;


    public void Init(float speed, float dmg, float exRng)
    {
        granadeDamage = dmg;
        granadeSpeed = speed;
        granadeExplodeRange = exRng;

        isInit = true;
    }
    void Start()
    {
        if (isInit) return; // early return?

        Debug.LogError("Bullet is not Initiated!");
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(granadeVector * (granadeSpeed * Time.deltaTime));

        timeCounter += Time.deltaTime;
        if (timeCounter >= granadeMaxTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) // bullet �����տ��� trigger�� ���ְ� �;���
    {
        // ������ �¾��� ��
        if (other.gameObject.layer == 7 || other.gameObject.layer == 6)
        {
            // �����ϴ� �Լ��� ȣ��
            Explode(granadeExplodeRange);
            Destroy(gameObject);
        }
    }

    private void Explode(float exRange)
    {
        var tempExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
        tempExplosion.GetComponent<Explosion>().Init(exRange);

    }
}

