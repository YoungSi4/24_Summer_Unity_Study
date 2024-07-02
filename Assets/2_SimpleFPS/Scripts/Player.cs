using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // ������ ��
    // 1. wasd space
    // 2. ���콺 �ü�ó��

    private Rigidbody rigid; // �� ������Ʈ ���ο��� �پ��ֱ� ������ private

    // ����
    private const int Maxhp = 3;
    public int healthpoint = Maxhp;

    public Image[] heartImages;

    public float moveSpeed;
    public float rotateXSpeed;
    public float rotateYSpeed;
    public float jumpForce;
    public bool enableJump = false;

    // �ǰ� �����ð�
    private bool invincible = false;
    
    // �Ӹ� ����
    public Transform headPivot;
    private float rotationX; // -90 ~ 90
    public bool hideCursor = true;


    // �Ѿ�
    public GameObject bullet;

    // ����
    // muzzle?
    public Weapon[] weapons;
    private Weapon curWeapon;
    public Transform muzzle;
    

    void Start()
    {
        rigid = GetComponent<Rigidbody>();

        if (hideCursor)
        {
            // ���콺 ���߱�
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            // ���� ����
            weapons[0].gameObject.SetActive(true);
            weapons[0].Init(muzzle);
            curWeapon = weapons[0];
        }
        
    }

    void Update()
    {
        Vector3 moveVector = Vector3.zero;
        // Vector3 moveVector = new Vector3(0f, 0f, 0f);�� ���� �ؾ��ϴµ�
        // ���� ���� �� ��������� �̹� �־�� �� �ִ�.

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



        moveVector.Normalize(); // �밢������ ���� ���̸� 1�� �ٲ��ִ� ����.

        // transform�� ������ �ʾƵ� ȣ�� ����. ��� ������Ʈ�� transform�� ������ �ֱ� ����.
        // GetComponent<Transform>();
        transform.Translate(moveVector * (moveSpeed * Time.deltaTime));



        // rotate

        // �¿�
        // Input.GetAxis("Mouse X"); // �� �����Ӵ� ���콺 x�� �̵����� ��ȯ
        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * rotateXSpeed); // �̹� ������ ���̶� deltatime ���ʿ�

        // ����
        rotationX -= Input.GetAxis("Mouse Y") * rotateYSpeed;
        Mathf.Clamp(rotationX, -90f, 90f); // ���� ����
        // headPivot.Rotate(Vector3.right, -Input.GetAxis("Mouse Y") * rotateYSpeed * Time.deltaTime);

        var tempRotation = headPivot.eulerAngles;
        tempRotation.x = rotationX;
        headPivot.rotation = Quaternion.Euler(tempRotation);


        


    }

    private void GetDamage(Vector3 enemyPos)
    {
        invincible = true;
        StartCoroutine(invincibleTimer()); // ��ŸƮ�ڷ�ƾ? Ÿ�̸ӹ�ư�� ������.
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
        // 2�� ��ٷ��ִ� �Լ�
        yield return new WaitForSeconds(2f);
        invincible = false;
    }
}
