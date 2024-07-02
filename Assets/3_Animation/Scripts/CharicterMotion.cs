using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharicterMotion : MonoBehaviour
{
    private Animator ani;
    public Transform camPivot;

    private Vector3 characterDir;
    private Vector2 moveVector;

    public float moveSpeed;

    private static readonly int IsRun = Animator.StringToHash("isRun");
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private static readonly int JumpTrg = Animator.StringToHash("jump");

    private void Start()
    {
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        camPivot.position = transform.position;

        var _3DDir = new Vector3(moveVector.x, 0f, moveVector.y);
        characterDir = Vector3.Slerp(characterDir, _3DDir, 1f); // ���� ������

        transform.Translate(characterDir * moveSpeed * Time.deltaTime, Space.World); // ���� �̵�


        // ���� ��ȯ
        transform.LookAt(transform.position + characterDir, Vector3.up);
    }

    // update�� if�� ������ �� �ƴ϶� �̷� ������ �ؼ� �����ϸ� �ȴ�.
    public void WalkForward(InputAction.CallbackContext context) 
    {
        if (context.started)
        {
            ani.SetBool(IsMoving, true);
            // �̰ź��� private static readonly bool IsMoving = Animator.StringToHash("isMoving") 
            moveVector = context.ReadValue<Vector2>();
        }

        if (context.canceled)
        {
            ani.SetBool(IsMoving, false);
            moveVector = Vector2.zero;
        }
    }

    public void RunPhase(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ani.SetBool(IsRun, true);
        }

        if (context.canceled)
        {
            ani.SetBool(IsRun, false);
        }
    }

    public void JumpPhase(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ani.SetTrigger(JumpTrg); // �ִϸ��̼� �÷���
        }
    }
}
