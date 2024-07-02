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

    // ������ Update���̶�� �����ص� ��
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
            // ���Ŀ� ���� ���� (�� or �� ����) -> wandering
            if (targetFood.IsDestroyed())
            {
                nextState = wanderingState;
                return true;

            }


            // ���Ŀ� ���� ����
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
            //    targetFood = null; // Ȯ���ϰ� targetFood �����ֱ�
            //    nextState = idleState;
            //    return true;
            //}


        }

        // �ƹ� �ش絵 ������?
        return false;
    }



    private bool CheckFoodInRange()
    {
        // if food in range?
        var foods = Physics.OverlapSphere(transform.position, 10f, 1 << 10);

        if (foods.Length < 1)
            return false;

        // ���� ����� ������ �Ա� -> Ž���� ���� �� �������� �Ա�
        //var min = float.MaxValue; // float �� ���� ū ��
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

    // Dove�� Hawk�� ���⼭ �ٸ�
    private void EatingEnter()
    {
        // ���� �߿� - �����캹�� �� ���� ����
        if(targetFood.CheckEnemy()) targetFood.SetOwner(this);
    }

    private void EatingUpdate()
    {
        eatingTimer += Time.deltaTime;

        if (eatingTimer > eatingRate)
        {
            // ���� ���� ��� ����ó��
            if (targetFood.IsDestroyed()) return;

            targetFood.TakeFood();
            hp++;
            eatingTimer = 0f;
        }

    }
}