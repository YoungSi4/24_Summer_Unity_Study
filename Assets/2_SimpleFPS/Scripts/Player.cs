using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // 구현할 거
    // 1. wasd space
    // 2. 마우스 시선처리

    private Rigidbody rigid; // 이 오브젝트 본인에게 붙어있기 떄문에 private

    // 스탯
    private const int Maxhp = 3;
    public int healthpoint = Maxhp;

    public Image[] heartImages;

    public float moveSpeed;
    public float rotateXSpeed;
    public float rotateYSpeed;
    public float jumpForce;
    public bool enableJump = false;

    // 피격 무적시간
    private bool invincible = false;
    
    // 머리 조작
    public Transform headPivot;
    private float rotationX; // -90 ~ 90
    public bool hideCursor = true;


    // 총알
    public GameObject bullet;

    // 무기
    // muzzle?
    public Weapon[] weapons;
    private Weapon curWeapon;
    public Transform muzzle;
    

    void Start()
    {
        rigid = GetComponent<Rigidbody>();

        if (hideCursor)
        {
            // 마우스 감추기
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            // 시작 무기
            weapons[0].gameObject.SetActive(true);
            weapons[0].Init(muzzle);
            curWeapon = weapons[0];
        }
        
    }

    void Update()
    {
        Vector3 moveVector = Vector3.zero;
        // Vector3 moveVector = new Vector3(0f, 0f, 0f);를 원래 해야하는데
        // 자주 쓰는 건 상수값으로 이미 넣어둔 게 있다.

        // move
        if(Input.GetKey(KeyCode.W))
        {
            moveVector += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveVector += Vector3.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveVector += Vector3.back;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveVector += Vector3.right;
        }

        if(Input.GetKeyDown(KeyCode.Space) && enableJump == true)
        {
            rigid.AddForce(0f, jumpForce, 0f, ForceMode.Impulse);
            enableJump = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectWeapon(2);



        moveVector.Normalize(); // 대각선으로 가도 길이를 1로 바꿔주는 역할.

        // transform은 만들지 않아도 호출 가능. 모든 오브젝트가 transform을 가지고 있기 때문.
        // GetComponent<Transform>();
        transform.Translate(moveVector * (moveSpeed * Time.deltaTime));



        // rotate

        // 좌우
        // Input.GetAxis("Mouse X"); // 한 프레임당 마우스 x축 이동량을 반환
        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * rotateXSpeed); // 이미 보정된 값이라 deltatime 불필요

        // 상하
        rotationX -= Input.GetAxis("Mouse Y") * rotateYSpeed;
        Mathf.Clamp(rotationX, -90f, 90f); // 각도 제한
        // headPivot.Rotate(Vector3.right, -Input.GetAxis("Mouse Y") * rotateYSpeed * Time.deltaTime);

        var tempRotation = headPivot.eulerAngles;
        tempRotation.x = rotationX;
        headPivot.rotation = Quaternion.Euler(tempRotation);


        


    }

    private void GetDamage(Vector3 enemyPos)
    {
        invincible = true;
        StartCoroutine(invincibleTimer()); // 스타트코루틴? 타이머버튼만 눌러줌.
        healthpoint--;

        var knockVector = (this.transform.position - enemyPos).normalized * 5f;
        knockVector += Vector3.up * 2f;
        rigid.AddForce(knockVector, ForceMode.Impulse);

        if (healthpoint < 0) return;

        heartImages[healthpoint].enabled = false;



        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 6)
        {
            enableJump = true;
        }

        if(collision.gameObject.layer == 7)
        {
            if (invincible) return;
            GetDamage(collision.transform.position);
        }
    }

    

    private void SelectWeapon(int idx)
    {
        curWeapon.gameObject.SetActive(false);

        weapons[idx].gameObject.SetActive(true);
        weapons[idx].Init(muzzle);

        curWeapon = weapons[idx];
    }


    private IEnumerator invincibleTimer()
    {
        // 2초 기다려주는 함수
        yield return new WaitForSeconds(2f);
        invincible = false;
    }
}
