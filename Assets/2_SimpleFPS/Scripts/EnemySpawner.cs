using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy; // 유니티에서 넣어주면 된다.
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
            spawnTimer -= spawnRate; // 0으로 초기화하면 프레임 일부가 날아간다. 이게 누적되면 의도대로 안 굴러감.

            var posX = Random.Range(-spawnRange, spawnRange);
            var posY = Random.Range(-spawnRange, spawnRange);
            var tempEnemy = Instantiate(enemy, new Vector3(posX, 0f, posY), Quaternion.identity).GetComponent<Enemy>(); // 쿼터니온.identity : 원형 그대로
            tempEnemy.Init(Random.Range(70f, 120f));
        }
    }
}
