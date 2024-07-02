using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
    private Rigidbody rigid; // 리지드바디 객체 호출
    public float jumpForce = 5.0f;
    public bool enableJump = false;
    public int countJump = 0;

    void Start()
    {
        rigid = GetComponent<Rigidbody>(); // 오브젝트의 컴포넌트 가져오기

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
    
    // 오브젝트가 collision이 발생했을 때
    private void OnCollisionEnter(Collision collision)
    {
        // layer는 bitflag로 구현되어 있다. ground가 6번이므로 1<<6
        if(collision.gameObject.layer == 6)
        {
            enableJump = true;
            countJump = 0;
        }
    }

    // 물체 크기가 이상하면 점프 입력도 안 했는데 false가 됨.
    // collision 상태가 해제될 떄.
    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.layer == 6)
    //    {
    //        enableJump = false;
    //    }
    //}
}
