using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // 다른 컴포넌트에 접근 - transform
    private Transform trans; // 컴포넌트는 클래스로 구현되어있다.
    public Vector3 move = new Vector3(0.05f, 0f, 0f); // Vector3는 구조체. 동적할당 필수
    // 기본 private. public은 기능이 하나 더 있음
    // public으로 지정하면 컴포넌트 상에서 값을 조작 가능
    public float speed;


    // Start is called before the first frame update
    void Start() // 최초 실행될 때?
                 // 정확히는 이 오브젝트가 활성화된 이후에 1회 실행
    {
        Debug.Log("Hello, Unity!");
        trans = GetComponent<Transform>(); // getcomponent는 비용이 꽤 든다.
        // start에서 한 번 캐싱을 하고, trans를 앞으로 사용하면 된다.
    }

    
    // Update is called once per frame
    void Update() // 프레임마다 실행되는 함수 (보통 초당 60 프레임)
                   // 프레임 간격이 일정하진 않음 -> 보정? deltatime 활용
    {
        // trans.Translate(move);
        // trans.Translate(0.005f, 0f, 0f); // 위치를 이동시키는 함수 x y z

        // deltatime은 프레임 간격이 짧아지면 속도를 올리고, 길어지면 속도를 낮춘다.
        // 그래서 기존에는 한 프레임당 이동거리를 적어줬다면,
        // deltatime을 곱해주면 초속을 적어주면 된다.

        if(Input.GetKey(KeyCode.W))
        {
            move = new Vector3(0f, 0f, speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            move = new Vector3(-speed * Time.deltaTime, 0f, 0f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            move = new Vector3(0f, 0f, -speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            move = new Vector3(speed * Time.deltaTime, 0f, 0f);
        }
        else
        {
            move = new Vector3(0f, 0f, 0f);
        }

        trans.Translate(move); // 위에서 벡터를 지정하고 여기서 실행
    }
}
