using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMstate // 컴포넌트로 사용하지 않을 예정이므로 mono 상속을 지움
    // 모노가 없어서 생성자 사용할 수 있다!
{
    // 함수로 state를 만들어두고, 델리게이트로 Blobs에 넘겨줄 예정
    public Action OnEnter; // Action 지정 안 하면 void 반환 매개변수 X
    public Action OnUpdate;
    public Action OnExit;


    // 생성자
    public FSMstate(Action onEnter, Action onUpdate, Action onExit) 
    {
        OnUpdate = onUpdate;
        OnEnter = onEnter;
        OnExit = onExit;
    }


}