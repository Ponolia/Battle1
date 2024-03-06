using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Monster : AIMoveMent
{
    public enum State
    {
        Create, Normal, Roaming, Battle, Dead
    }
    public State myState = State.Create;

    public bool isRespawn = true;
    public float respawnTime = 3.0f;

    EnemySpawner spawner = null;

    Vector3 startPos = Vector3.zero;
    GameObject hpBarObj = null;


    void ChangeState(State s)
    {
        if (myState == s || myState == State.Dead) return;
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
                // myTarget = myPerception.myTarget;
                AttackTargeting(myPerception.myTarget);
                break;
            case State.Dead:
                StopAllCoroutines();
                myAnim.SetTrigger("Die");
                DisAppear();
              // myCol.enabled = false;
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
    public override void OnDamage(float dmg, Vector3 attackVec, float knockBackDist, bool isDown)
    {
        ////curHP -= dmg;
        if (myTarget == null) myTarget = myPerception.myTarget;
        // GameManager.Inst.inGameManager.myPlayer.transform;
        ChangeState(State.Battle);
        
        base.OnDamage(dmg, attackVec, knockBackDist, isDown);

        if (!IsLive)
        {
            ChangeState(State.Dead);
        }
    }
    void Awake()
    {
        startPos = transform.position; // 스타트 지점 저장
        if (transform.parent != null)
            transform.parent.TryGetComponent<EnemySpawner>(out spawner);
    }

    void OnEnable()
    {
        OnReset();
    }

    public void OnReset()
    {
        Initialize();
        GameObject res = Resources.Load<GameObject>("UI\\EnemyHpBar");
        hpBarObj = Instantiate(Resources.Load("UI\\EnemyHpBar") as GameObject,
            GameObject.Find("DynamicCanvas").transform.GetChild(1));
        myHpBar = hpBarObj.GetComponent<Slider>();
        hpBarObj.GetComponent<EnemyHPBar>().SetTarget(transform);
        StartCoroutine(StartDelaying(2.0f));
    }

    IEnumerator StartDelaying(float t)
    {
        yield return new WaitForSeconds(t);
        ChangeState(State.Normal);
        myPerception.gameObject.SetActive(true);
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
        Destroy(hpBarObj);
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
        //리스폰 할지 안할지
        if (isRespawn && spawner != null)
        {
            //리스폰
            spawner.Respawn(this, respawnTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Respawn()
    {
        transform.position = startPos;
        myCol.enabled = true;
        myPerception.gameObject.SetActive(false);
       myState = State.Normal;
        ChangeState(State.Create);
    }
}
