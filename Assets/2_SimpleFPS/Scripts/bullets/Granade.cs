using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour
{
    // 스탯
    private float granadeSpeed;
    private float granadeDamage;
    private float granadeExplodeRange;

    // 방향
    private bool isInit = false;
    private Vector3 granadeVector = Vector3.forward;

    // 소멸
    private float timeCounter = 0f;
    private const float granadeMaxTime = 5f;

    // 피격 시 vfx
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

    private void OnTriggerEnter(Collider other) // bullet 프리팹에서 trigger를 켜주고 와야함
    {
        // 적에게 맞았을 때
        if (other.gameObject.layer == 7 || other.gameObject.layer == 6)
        {
            // 폭발하는 함수를 호출
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

