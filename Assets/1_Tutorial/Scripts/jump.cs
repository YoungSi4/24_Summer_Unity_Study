using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
    private Rigidbody rigid; // ������ٵ� ��ü ȣ��
    public float jumpForce = 5.0f;
    public bool enableJump = false;
    public int countJump = 0;

    void Start()
    {
        rigid = GetComponent<Rigidbody>(); // ������Ʈ�� ������Ʈ ��������

    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && enableJump)
        {
            rigid.AddForce(0f, jumpForce, 0f, ForceMode.Impulse);
            countJump++;
        }

        if (countJump > 1) enableJump = false;
    }
    
    // ������Ʈ�� collision�� �߻����� ��
    private void OnCollisionEnter(Collision collision)
    {
        // layer�� bitflag�� �����Ǿ� �ִ�. ground�� 6���̹Ƿ� 1<<6
        if(collision.gameObject.layer == 6)
        {
            enableJump = true;
            countJump = 0;
        }
    }

    // ��ü ũ�Ⱑ �̻��ϸ� ���� �Էµ� �� �ߴµ� false�� ��.
    // collision ���°� ������ ��.
    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.layer == 6)
    //    {
    //        enableJump = false;
    //    }
    //}
}
