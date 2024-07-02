using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float exploisonRange;
    public float explosionSpeed;
    private float explosionDuration = 1.5f;
    private float time = 0f;
    private bool isInit = false;

    public void Init(float expRng)
    {
        exploisonRange = expRng;
        isInit = true;
    }

    void Start()
    {
        if (isInit)
        {
            Debug.Log("explosion is not initiated");
            Destroy(gameObject);
        }
    }

    void Update()
    {
        
        if (transform.localScale.x < exploisonRange)
        {
            transform.localScale += new Vector3(explosionSpeed * Time.deltaTime, explosionSpeed * Time.deltaTime, explosionSpeed * Time.deltaTime);
        }
        else
        {
            time += Time.deltaTime;
            if (time > explosionDuration)
            {
                Destroy(gameObject);
            }
        }
    }
}
