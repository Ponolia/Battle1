using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCon : AIMoveMent
{
    public enum State
    {
        Create, Normal, Roaming, Battle, Dead
    }
    public State myState = State.Create;

    Vector3 startPos = Vector3.zero;
    public Transform barPoint = null;
    GameObject hpBarObj = null;
    void ChangeState(State s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case State.Normal:
               
                    Vector3 rndDir = Vector3.forward;
                    Quaternion rndRot = Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0);
                    float dist = Random.Range(0.0f, 5.0f);
                    rndDir = rndRot * rndDir * dist;
                    Vector3 rndPos = startPos + rndDir;

                    MovetoPos(rndPos, () => StartCoroutine(Waiting(Random.Range(1.0f, 3.0f))));
                    ChangeState(State.Roaming);
                
                break;
            case State.Battle:
                AttackTarget(myPerception.myTarget);
                break;
            case State.Dead:
                myPerception.enabled = false;
                StopAllCoroutines();
                gameObject.SetActive(false);
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

    private void Awake()
    {
        startPos = transform.position; // ��ŸƮ ���� ����

        ChangeState(State.Normal);
    }
   
    private void OnEnable()
    {
        Initialize();
        hpBarObj = Instantiate(Resources.Load("UI\\EnemyHPBar") as GameObject,
          GameObject.Find("Canvas").transform.GetChild(0));
        myHpBar = hpBarObj.GetComponent<Slider>();
        hpBarObj.GetComponent<EnemyHPBar>().SetTarget(transform);
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
        ChangeState(State.Normal);
    }
    public override void OnDamage(float dmg)
    {
        base.OnDamage(dmg);
        if (!IsLive)
        {
            ChangeState(State.Dead);
        }
    }
    protected override void OnDead()
    {
        ChangeState(State.Dead);
    }
    public void DisAppear()
    {
        Destroy(hpBarObj);
        StartCoroutine(DisAppearing(0.5f, 2.0f));
    }
    IEnumerator DisAppearing(float speed,float t)
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
