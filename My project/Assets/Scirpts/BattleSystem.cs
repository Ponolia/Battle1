using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IDamage
{
    void OnDamage(float dmg, Vector3 attackVec, float knockBackDist, bool isDown);
}

public interface ILive
{
    bool IsLive
    {
        get;
    }
}
[System.Serializable]
public struct BattleStat
{
    public int LV;
    public float MaxHpPoint;
    public float MaxMpPoint;
    public int MaxExp;
    public float AttackPoint;
    public float DefensePoint;
    public float AttackRange;
    public float AttackDelay;
    public float AttackSize;
}
public class BattleSystem : MoveMent, IDamage, ILive
{
    public List<Item> myItem;
    [SerializeField] Transform myAttackArea = null;
   
    [SerializeField] protected LayerMask enemyMask;
    protected Transform myTarget = null;
    protected virtual void Initialize() 
    { 
        curHP = battleStat.MaxHpPoint;
        curMP = battleStat.MaxMpPoint;
        curExp = 0;
        curAttackPoint = battleStat.AttackPoint;
        curDefensePoint = battleStat.DefensePoint;
    }
    public bool IsLive
    {
        get
        {
            return curHP > 0.0f;
        }
    }
    public bool IsLvUP
    {
        get
        {
            return curExp >= battleStat.MaxExp;
        }
    }
    public virtual void OnAttack() 
    {
        BattleManager.AttackDirCircle(myAttackArea.position,
             battleStat.AttackSize,
             enemyMask,
             curAttackPoint,
             transform.forward,
             false, 0.5f);
    }
    public virtual void OnDamage(float dmg, Vector3 attackVec, float knockBackDist, bool isDown)
    {
        float damage = dmg - curDefensePoint;
        damage = damage <= 1 ? 1 : damage;
        curHP -= damage;
        if (!isDown)
        {
            OnCharStagger();
        }
        else
        {
            OnCharDown();
        }
        KnockBack(attackVec, knockBackDist);
        BattleManager.DamagePopup(transform, damage);
    }
    protected virtual void OnCharStagger()
    {
       // StopMove();
       myAnim.SetTrigger("Hit");
        myAnim.Play("Hit", -1, 0f);

    }
    protected virtual void OnDead()
    {

    }
    protected virtual void OnCharDown()
    {

    }
    void KnockBack(Vector3 attackVec, float knockBackDist)
    {
        transform.forward = -attackVec;
        //transform.Translate(attackVec * knockBackDist, Space.World);
        StartCoroutine(Moving(attackVec * knockBackDist));
    }
    IEnumerator Moving(Vector3 dir)
    {
        float dist = dir.magnitude;
        dir.Normalize();

        while (dist > 0.0f)
        {
            float delta = Time.deltaTime * 30.0f;   //�˹� �ӵ� �ϴ� ����. 30.0f
            if (delta > dist) delta = dist;
            dist -= delta;

            transform.Translate(dir * delta, Space.World);

            if (UnityEngine.AI.NavMesh.SamplePosition(transform.position, out UnityEngine.AI.NavMeshHit hit, 5.0f, UnityEngine.AI.NavMesh.AllAreas))
            {
                transform.position = hit.position;
            }
            yield return null;
        }
    }
    public void DropExp(int Exp)
    {
        //int Exp = Random.Range(50, 101);
        GameManager.Inst.inGameManager.myPlayer.curExp += Exp;
        if (GameManager.Inst.inGameManager.myPlayer.IsLvUP)
        {
            int less = (int)(curExp - battleStat.MaxExp);
            GameManager.Inst.inGameManager.myPlayer.LevelUp();
            GameManager.Inst.inGameManager.myPlayer.curExp += less;
        }
    }
    
}
