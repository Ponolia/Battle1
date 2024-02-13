using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCon : AIMoveMent
{
    public enum State
    {
        Create, Normal, Roaming, Battle
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
                //if (IsRoaming)
                //{
                    Vector3 rndDir = Vector3.forward;
                    Quaternion rndRot = Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0);
                    float dist = Random.Range(0.0f, 5.0f);
                    rndDir = rndRot * rndDir * dist;
                    Vector3 rndPos = startPos + rndDir;

                    MovetoPos(rndPos, ()=> StartCoroutine(Waiting(Random.Range(1.0f,3.0f))));
                    ChangeState(State.Roaming);
              //  }
                break;
            case State.Roaming:
                break;
            case State.Battle:
                AttackTarget(myPerception.myTarget);
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

    void Start()
    {
        startPos = transform.position; // 스타트 지점 저장
        ChangeState(State.Normal);
    }
    void Update()
    {
        StateProcess();
    }
    public void FindEnemy()
    {
        ChangeState(State.Battle);
    }
    public void LostEnemy()
    {
        myTarget = myPerception.myTarget;
        ChangeState(State.Battle);
    }
  
}
