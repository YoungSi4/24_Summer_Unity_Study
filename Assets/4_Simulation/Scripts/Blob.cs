using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Blob : MonoBehaviour
{
    // States: Idle, Wandering, Tracing, Eating
    protected FSMstate idleState;
    protected FSMstate wanderingState;
    protected FSMstate tracingState;
    protected FSMstate eatingState;


    protected FSMstate curState;
    protected FSMstate nextState;

    // flag
    private bool isTransit;

    // stats
    protected int hp = 20;
    protected const int maxHp = 40; // 번식
    
    // food
    protected Food targetFood;

    // agent
    protected NavMeshAgent agent;

    // 객체 미리 할당
    private WaitForSeconds energyUseRate = new WaitForSeconds(2);

    public GameObject blobPrefab;

    // 자손 스탯들
    // 얘는 변경 간격
    public float idleTime = 1f;
    protected float idleTimer;

    protected float eatingTimer;
    protected float eatingRate = 1f;

    // 도착했는지 확인 필요
    public Vector3 WanderingPos;


    private void Awake()
    {
        StateInit(); // when awake get started, intialize states.
        // allocate agent (it's not public)
        agent = GetComponent<NavMeshAgent>();

        
        StartCoroutine(UsingEnergy());
    }


    private void Update()
    {
        // 이전 프레임에서 걸어둔 nextState를 받아옴
        if (isTransit)
        {
            curState = nextState;
            curState.OnEnter?.Invoke();
            isTransit = false;
        }

        // 여기서 대신 코드를 대신 돌려줌
        // state check
        curState.OnUpdate?.Invoke();
        isTransit = TransitionCheck(); // 모든 blob들은 체크를 하는데 자식에 따라 방식이 다름
        
        // transit or not
        // if true, exit the curState, and get a nextState
        if (isTransit) curState.OnExit?.Invoke();

    }

    // return transit or not
    protected abstract bool TransitionCheck();

    // state initializing
    protected abstract void StateInit();

    public void ResetFood()
    {
        targetFood = null;
    }

    private IEnumerator UsingEnergy()
    {
        while(hp > 0) // while blob is alive
        {
            yield return energyUseRate; // 2sec
            hp--;

            // maxHp 이상이면 자가분열
            if(hp >= maxHp)
            {
                Instantiate(blobPrefab, transform.position, Quaternion.identity);
                hp -= 20; // 20만큼 떼서 번식

                if (gameObject.CompareTag("Dove"))
                {
                    SimulationManager.Instance.DoveCount++;
                }
                else
                {
                    SimulationManager.Instance.HawkCount++;
                }
            }
            
        }

        // while문 밖으로 나오는 것 자체가 체력이 0이 되어 죽은 것
        // 먹다가 죽을 수도 있으므로 예외처리
        if (targetFood != null && targetFood.IsDestroyed())
            targetFood.RemoveOwner(this);
        Destroy(gameObject);
        if (gameObject.CompareTag("Dove"))
        {
            SimulationManager.Instance.DoveCount--;
        }
        else
        {
            SimulationManager.Instance.HawkCount--;
        }
    }
}
