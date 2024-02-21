using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : AIMoveMent
{

    public enum State
    {
        Create, Normal, Roaming, Battle, Dead
    }
    public State myState = State.Create;


    NavMeshAgent agent;
    Vector3 startPos = Vector3.zero;
    void ChangeState(State s)
    {
        if (myState == s || myState == State.Dead ) return;
        myState = s;
        switch (myState)
        {
            case State.Normal:
                StopAllCoroutines();
                MovetoPos(startPos);
                break;
            case State.Roaming:
                ChangeState(State.Normal);
                break;
            case State.Battle:
                StopAllCoroutines();
                myTarget = myPerception.myTarget;
                AttackTarget(myPerception.myTarget);
                break;
            case State.Dead:
                // myCol.enabled = false;
                StopAllCoroutines();
                DisAppear();

                break;
        }
    }
    IEnumerator Waiting(float t)
    {
        yield return new WaitForSeconds(t);
        ChangeState(State.Normal);
    }
    void StateProcess()
    {
        switch (myState)
        {
            case State.Normal:
                break;

            case State.Battle:
                break;
        }
    }
    public override void OnDamage(float dmg)
    {
        curHP -= dmg;
        if (myPerception.myTarget != null)
        {
            ChangeState(State.Battle);
        }
        base.OnDamage(dmg);
    }

    void Start()
    {
        startPos = transform.position; // 스타트 지점 저장
        ChangeState(State.Normal); Initialize();
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        StateProcess();
    }
    public void FindEnemy()
    {
      
    }
    public void LostEnemy()
    {
        myTarget = myPerception.myTarget;
        ChangeState(State.Roaming);
    }
    protected override void OnDead()
    {
        ChangeState(State.Dead);
    }
    public void DisAppear()
    {
       // Destroy(hpBarObj);
        StartCoroutine(DisAppearing(0.5f, 2.0f));
    }
    IEnumerator DisAppearing(float speed, float t)
    {
        yield return new WaitForSeconds(t);
        float dist = 2.0f;
        while (dist > 0.0f)
        {
            float delta = speed * Time.deltaTime;
            dist -= delta;
            transform.Translate(Vector3.down * delta, Space.World);
            yield return null;
        }
        Destroy(gameObject);
    }
}
