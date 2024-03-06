using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : BattleSystem
{
    public Transform attackArea1;
    public Transform attackArea2;
    public Transform attackArea3;

    public GameObject exitPortal;

    private void Start()
    {
        base.Initialize();

    }

    public override void OnDamage(float dmg, Vector3 attackVec, float knockBackDist, bool isDown)
    {
        //���� ���ʹ� ��������
        float damage = dmg - curDefensePoint;
        damage = damage <= 1 ? 1 : damage;
        curHP -= damage;

     //   BattleManager.DamagePopup(transform, damage);

        if (!IsLive)
        {
            //���� ����, Ŭ����
            //���� �״� �ִϸ��̼�
            //HP�� ���ֱ�
            //�ƾ� ��, ������ ���, �� ������ ����, �ⱸ ����
            StopAllCoroutines();
            StartCoroutine(StartDeadCoroutine());
        }
    }
    IEnumerator StartDeadCoroutine()
    {
        myAnim.SetTrigger("Die");
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(2.0f);

        GameManager.Inst.UiManager.myBossHpBar.SetActive(false);
        DropExp(battleStat.MaxExp);
        //DropItem();
       // exitPortal.SetActive(true);

        ////����Ʈ ����
        //GameManager.Inst.questManager.ProcessQuest(QuestType.DestroyEnemy, ID);
    }
    //public void DropItem()
    //{
    //    GameObject obj = Instantiate(Resources.Load("DropItem") as GameObject);
    //    obj.GetComponent<ItemDrop>().OnSetItem(this.gameObject.transform);
    //}

    public void OnStartCutScene()
    {
        myTarget = GameManager.Inst.inGameManager.myPlayer.transform;

        myAnim.SetTrigger("Start");
    }
    public void OnEndCutScene()
    {
        myAnim.SetBool("CanMove", true);
        AttackTargets(myTarget);

        //HP�� ����
        //GameManager.Inst.UiManager.myBossHpBar.SetActive(true);
    }

    //�̵� �� ����
    void AttackTargets(Transform target)
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
                    RandomAttack(ref playTime);
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

    void RandomAttack(ref float playTime)
    {
        int rndValue = Random.Range(1, 5);
        switch (rndValue)
        {
            case 1:
                myAnim.SetTrigger("Attack");
                playTime = 2.0f;
                break;
            case 2:
                myAnim.SetTrigger("Attack1");
                playTime = 2.0f;
                break;
            case 3:
                myAnim.SetTrigger("Attack2");
                playTime = 3.0f;
                break;
            case 4:
                myAnim.SetTrigger("Attack3");
                playTime = 3.0f;
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
}
