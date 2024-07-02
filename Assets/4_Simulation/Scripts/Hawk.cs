using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hawk : Blob
{
    protected override void StateInit()
    {
        idleState = new FSMstate(IdleEnter, null, null);
        wanderingState = new FSMstate(WanderingEnter, null, null);
        tracingState = new FSMstate(TracingEnter, null, null);
        eatingState = new FSMstate(EatingEnter, EatingUpdate, null);

        curState = idleState;
    }

    // 일종의 Update문이라고 생각해도 됨
    protected override bool TransitionCheck()
    {
        // idle
        if (curState == idleState)
        {
            // counting time
            idleTimer += Time.deltaTime;

            // checking time
            if (idleTimer > idleTime)
            {
                // make decision to transit
                nextState = wanderingState;
                return true;
            }

            return CheckFoodInRange();
        }


        // wandering
        else if (curState == wanderingState)
        {
            if (Vector3.Distance(transform.position, WanderingPos) < 1.1f)
            {
                nextState = idleState;
                return true;
            }

            return CheckFoodInRange();
        }


        // tracing
        else if (curState == tracingState)
        {
            // 음식에 도달 실패 (매 or 다 먹음) -> wandering
            if (targetFood.IsDestroyed())
            {
                nextState = wanderingState;
                return true;

            }


            // 음식에 도달 성공
            if (Vector3.Distance(transform.position, targetFood.transform.position) < 1.1f)
            {
                nextState = eatingState;
                return true;
            }

        }

        // eating
        else if (curState == eatingState)
        {
            if (targetFood.IsDestroyed())
            {
                targetFood = null;
                nextState = idleState;
                return true;
            }

            //if (targetFood.IsHawkEating())
            //{
            //    targetFood = null; // 확실하게 targetFood 지워주기
            //    nextState = idleState;
            //    return true;
            //}


        }

        // 아무 해당도 없으면?
        return false;
    }



    private bool CheckFoodInRange()
    {
        // if food in range?
        var foods = Physics.OverlapSphere(transform.position, 10f, 1 << 10);

        if (foods.Length < 1)
            return false;

        // 가장 가까운 음식을 먹기 -> 탐지된 음식 중 랜덤으로 먹기
        //var min = float.MaxValue; // float 중 가장 큰 값
        //for (int i = 0; i < foods.Length; i++)
        //{
        //    var dist = Vector3.Distance(transform.position, foods[i].transform.position);
        //    if (dist < min)
        //    {
        //        min = dist;
        //        targetFood = foods[i].GetComponent<Food>();
        //    }
        //}

        targetFood = foods[Random.Range(0, foods.Length)].GetComponent<Food>();

        nextState = tracingState;
        return true;
    }


    private void IdleEnter()
    {
        idleTimer = 0f;
    }

    private void WanderingEnter()
    {
        float mapSize = SimulationManager.Instance.mapSize;

        var targetPos = Random.insideUnitSphere * 5f + transform.position;
        targetPos.x = Mathf.Clamp(targetPos.x, -mapSize, mapSize);
        targetPos.y = 0f;
        targetPos.z = Mathf.Clamp(targetPos.z, -mapSize, mapSize);

        WanderingPos = targetPos;

        agent.SetDestination(targetPos);
    }

    private void TracingEnter()
    {
        if (targetFood.IsDestroyed()) return;

        agent.SetDestination(targetFood.transform.position);
    }

    // Dove랑 Hawk랑 여기서 다름
    private void EatingEnter()
    {
        // 순서 중요 - 쉐도우복싱 할 수도 있음
        if(targetFood.CheckEnemy()) targetFood.SetOwner(this);
    }

    private void EatingUpdate()
    {
        eatingTimer += Time.deltaTime;

        if (eatingTimer > eatingRate)
        {
            // 같이 먹을 경우 예외처리
            if (targetFood.IsDestroyed()) return;

            targetFood.TakeFood();
            hp++;
            eatingTimer = 0f;
        }

    }
}