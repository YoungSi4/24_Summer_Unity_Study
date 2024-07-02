using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float enemyHp;
    private float maxEnemyHp;
    private NavMeshAgent agent;

    private bool isArrived = true;
    private Vector3 targetPos;
    private const float moveRange = 5f;

    public GameObject hpBarPreFab;
    private HpBar hpBar;

    public void Init(float hp)
    {
        enemyHp = hp;
        maxEnemyHp = hp;
    }

    public void GetDamage(float dmg)
    {
        enemyHp -= dmg;
        hpBar.SetHpBar(enemyHp / maxEnemyHp);

        if (enemyHp <= 0 )
        {
            // 죽는 모션 실행
            Destroy(gameObject);
            Destroy(hpBar.gameObject);
        }

    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        // agent.SetDestination(new Vector3(0f, 0f, 0f));  

        // hpBar는 HpBar이고 hpBarPreFab은 게임오브젝트임. 게임 오브젝트에서 컴포넌트를 추가로 호출.
        hpBar = Instantiate(hpBarPreFab, GameManager.instance.canvsTransform).GetComponent<HpBar>();
    }

    void Update()
    {
        if (isArrived)
        {
            var randPos = Random.insideUnitCircle;
            targetPos = new Vector3(randPos.x, 0f, randPos.y) * moveRange + transform.position;
            agent.SetDestination(targetPos);
            isArrived = false;
        }

        if (Vector3.Distance(targetPos, transform.position) <= 1.1f)
        {
            isArrived = true;
        }

        hpBar.SetPosition(transform.position + Vector3.up * 2f); // 적 머리 위 2정도 띄움.
    }
}
