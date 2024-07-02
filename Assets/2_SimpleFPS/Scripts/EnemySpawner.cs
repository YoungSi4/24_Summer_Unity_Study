using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy; // ����Ƽ���� �־��ָ� �ȴ�.
    private const float spawnRange = 50f;
    public float spawnRate;
    private float spawnTimer = 0f;


    void Start()
    {
        
    }

    
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer > spawnRate)
        {
            spawnTimer -= spawnRate; // 0���� �ʱ�ȭ�ϸ� ������ �Ϻΰ� ���ư���. �̰� �����Ǹ� �ǵ���� �� ������.

            var posX = Random.Range(-spawnRange, spawnRange);
            var posY = Random.Range(-spawnRange, spawnRange);
            var tempEnemy = Instantiate(enemy, new Vector3(posX, 0f, posY), Quaternion.identity).GetComponent<Enemy>(); // ���ʹϿ�.identity : ���� �״��
            tempEnemy.Init(Random.Range(70f, 120f));
        }
    }
}
