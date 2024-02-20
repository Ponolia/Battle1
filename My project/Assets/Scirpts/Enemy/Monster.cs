using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : AIMoveMent
{

    public enum State
    {
        Create, Normal, Roaming, Battle, Dead
    }
    public State myState = State.Create;

    public bool IsRoaming;

    Vector3 startPos = Vector3.zero;
    void ChangeState(State s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case State.Normal:
                
                break;
            case State.Roaming:
                transform.LookAt(startPos);
                myAnim.SetBool("IsMove", true);
                float dist = Vector3.Distance(transform.position, startPos);
                this.transform.position = Vector3.MoveTowards(transform.position, startPos, Time.deltaTime * 3.0f);
                if (dist <= 0.1f)
                {
                    myAnim.SetBool("IsMove", false);
                }
                ChangeState(State.Normal);
                break;
            case State.Battle:
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
