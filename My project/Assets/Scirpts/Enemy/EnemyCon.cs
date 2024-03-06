using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCon : AIMoveMent
{
    public Transform attackArea1;
    public Transform attackArea2;
    public Transform attackArea3;
    public enum State
    {
        Create, Normal, Roaming, Battle, Dead
    }
    public State myState = State.Create;

    Vector3 startPos = Vector3.zero;

    GameObject hpBarObj = null;
    void ChangeState(State s)
    {
        if (myState == s || myState == State.Dead) return;
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
            case State.Roaming:
                ChangeState(State.Normal);
                break;
            case State.Battle:
                New_AttackTarget(myTarget);
                // AttackTarget(myPerception.myTarget);
                break;
            case State.Dead:
                StopAllCoroutines();
                StartCoroutine(StartDeadCoroutine());
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
        startPos = transform.position; // 스타트 지점 저장

        ChangeState(State.Normal);
    }

    private void OnEnable()
    {
        Initialize();

        hpBarObj = Instantiate(Resources.Load("UI\\EnemyHPBar") as GameObject,
          GameObject.Find("DynamicCanvas").transform.GetChild(0));
        myHpBar = hpBarObj.GetComponent<Slider>();
        hpBarObj.GetComponent<EnemyHPBar>().SetTarget(transform);
    }
    void Update()
    {
        StateProcess();
    }
    public override void OnDamage(float dmg, Vector3 attackVec, float knockBackDist, bool isDown)
    {
        //보스 몬스터는 데미지만
        float damage = dmg - curDefensePoint;
        damage = damage <= 1 ? 1 : damage;
        curHP -= damage;

        //   BattleManager.DamagePopup(transform, damage);

        if (!IsLive)
        {
            //보스 죽음, 클리어
            //보스 죽는 애니메이션
            //HP바 없애기
            //컷씬 후, 아이템 드랍
            ChangeState(State.Dead);
        }
    }
    public void FindEnemy()
    {
        myTarget = myPerception.myTarget;
        ChangeState(State.Battle);
    }
    public void LostEnemy()
    {
        myTarget = myPerception.myTarget;
        ChangeState(State.Normal);
    }

    protected override void OnDead()
    {
        ChangeState(State.Dead);
    }
    //public void DisAppear()
    //{
    //    Destroy(hpBarObj);
    //    StartCoroutine(DisAppearing(0.5f, 2.0f));
    //}
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
    void New_AttackTarget(Transform target)
    {
        if (target != null)
            StartCoroutine(Attacking(target));
    }
    IEnumerator Attacking(Transform target)
    {
        //playTime = 1.0f;
        while (true)
        {
            playTime += Time.deltaTime;
            Vector3 dir = target.position - transform.position;
            float dist = dir.magnitude - battleStat.AttackRange;
            if (dist < 0.01f) dist = 0.0f;
            dir.Normalize();

            float delta = moveSpeed * Time.deltaTime;

            if (!Mathf.Approximately(dist, 0.0f))
            {
                myAnim.SetBool("IsMove", true);
                if (delta > dist) delta = dist;
                if (!myAnim.GetBool("IsAttack"))
                {
                    transform.Translate(dir * delta, Space.World);
                }
            }
            else
            {
                myAnim.SetBool("IsMove", false);
                if (playTime >= battleStat.AttackDelay)
                {
                    playTime = 0.0f;
                    RandomAttack();
                }
            }

            float angle = Vector3.Angle(dir, transform.forward);
            float rotDir = 1.0f;
            if (Vector3.Dot(dir, transform.right) < 0.0f)
            {
                rotDir = -1.0f;
            }
            delta = rotSpeed * Time.deltaTime;
            if (!Mathf.Approximately(angle, 0.0f))
            {
                if (delta > angle) delta = angle;
                transform.Rotate(Vector3.up * delta * rotDir);
            }
            yield return null;
        }
    }
    void RandomAttack()
    {
        int rndValue = Random.Range(1, 5);
        switch (rndValue)
        {
            case 1:
                myAnim.SetTrigger("Attack");

                break;
            case 2:
                myAnim.SetTrigger("Attack1");

                break;
            case 3:
                myAnim.SetTrigger("Attack2");

                break;
            case 4:
                myAnim.SetTrigger("Attack3");

                break;
        }
    }

    public void BossAttack1()
    {
        BattleManager.AttackDirCircle(transform.position, 2.0f, enemyMask, 20.0f,
            transform.forward, false, 2.0f);
    }
    public void BossAttack2()
    {
        BattleManager.AttackDirCircle(transform.position, 2.0f, enemyMask, 20.0f,
            transform.forward, false, 2.0f);
    }
    public void BossAttack3()
    {
        BattleManager.AttackDirCircle(transform.position, 2.5f, enemyMask, 50.0f,
            transform.forward, false, 4.0f);
    }
    public void BossAttack4()
    {
        BattleManager.AttackCircle(transform.position, 5.0f, enemyMask, 40.0f,
            false, 4.0f);
    }
    IEnumerator StartDeadCoroutine()
    {
        myAnim.SetTrigger("Die");
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(2.0f);

        GameManager.Inst.UiManager.myBossHpBar.SetActive(false);
        DropExp(battleStat.MaxExp);
        // DropItem();
    }
}
