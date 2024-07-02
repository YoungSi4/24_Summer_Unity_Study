using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMstate // ������Ʈ�� ������� ���� �����̹Ƿ� mono ����� ����
    // ��밡 ��� ������ ����� �� �ִ�!
{
    // �Լ��� state�� �����ΰ�, ��������Ʈ�� Blobs�� �Ѱ��� ����
    public Action OnEnter; // Action ���� �� �ϸ� void ��ȯ �Ű����� X
    public Action OnUpdate;
    public Action OnExit;


    // ������
    public FSMstate(Action onEnter, Action onUpdate, Action onExit) 
    {
        OnUpdate = onUpdate;
        OnEnter = onEnter;
        OnExit = onExit;
    }


}