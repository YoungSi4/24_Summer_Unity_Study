using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // �ٸ� ������Ʈ�� ���� - transform
    private Transform trans; // ������Ʈ�� Ŭ������ �����Ǿ��ִ�.
    public Vector3 move = new Vector3(0.05f, 0f, 0f); // Vector3�� ����ü. �����Ҵ� �ʼ�
    // �⺻ private. public�� ����� �ϳ� �� ����
    // public���� �����ϸ� ������Ʈ �󿡼� ���� ���� ����
    public float speed;


    // Start is called before the first frame update
    void Start() // ���� ����� ��?
                 // ��Ȯ���� �� ������Ʈ�� Ȱ��ȭ�� ���Ŀ� 1ȸ ����
    {
        Debug.Log("Hello, Unity!");
        trans = GetComponent<Transform>(); // getcomponent�� ����� �� ���.
        // start���� �� �� ĳ���� �ϰ�, trans�� ������ ����ϸ� �ȴ�.
    }

    
    // Update is called once per frame
    void Update() // �����Ӹ��� ����Ǵ� �Լ� (���� �ʴ� 60 ������)
                   // ������ ������ �������� ���� -> ����? deltatime Ȱ��
    {
        // trans.Translate(move);
        // trans.Translate(0.005f, 0f, 0f); // ��ġ�� �̵���Ű�� �Լ� x y z

        // deltatime�� ������ ������ ª������ �ӵ��� �ø���, ������� �ӵ��� �����.
        // �׷��� �������� �� �����Ӵ� �̵��Ÿ��� ������ٸ�,
        // deltatime�� �����ָ� �ʼ��� �����ָ� �ȴ�.

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

        trans.Translate(move); // ������ ���͸� �����ϰ� ���⼭ ����
    }
}
