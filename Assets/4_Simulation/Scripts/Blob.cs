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
    protected const int maxHp = 40; // ����
    
    // food
    protected Food targetFood;

    // agent
    protected NavMeshAgent agent;

    // ��ü �̸� �Ҵ�
    private WaitForSeconds energyUseRate = new WaitForSeconds(2);

    public GameObject blobPrefab;

    // �ڼ� ���ȵ�
    // ��� ���� ����
    public float idleTime = 1f;
    protected float idleTimer;

    protected float eatingTimer;
    protected float eatingRate = 1f;

    // �����ߴ��� Ȯ�� �ʿ�
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
        // ���� �����ӿ��� �ɾ�� nextState�� �޾ƿ�
        if (isTransit)
        {
            curState = nextState;
            curState.OnEnter?.Invoke();
            isTransit = false;
        }

        // ���⼭ ��� �ڵ带 ��� ������
        // state check
        curState.OnUpdate?.Invoke();
        isTransit = TransitionCheck(); // ��� blob���� üũ�� �ϴµ� �ڽĿ� ���� ����� �ٸ�
        
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

            // maxHp �̻��̸� �ڰ��п�
            if(hp >= maxHp)
            {
                Instantiate(blobPrefab, transform.position, Quaternion.identity);
                hp -= 20; // 20��ŭ ���� ����

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

        // while�� ������ ������ �� ��ü�� ü���� 0�� �Ǿ� ���� ��
        // �Դٰ� ���� ���� �����Ƿ� ����ó��
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
