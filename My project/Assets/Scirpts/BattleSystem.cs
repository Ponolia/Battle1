using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IDamage
{
    void OnDamage(float dmg);
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
    public float MaxHpPoint;
    public float AttackPoint;
    public float AttackRange;
    public float AttackDelay;
}
public class BattleSystem : PlayerMove, IDamage, ILive
{
    protected Transform myTarget = null;
    protected void Initialize()
    {
        curHP = battleStat.MaxHpPoint;
    }
    public bool IsLive
    {
        get
        {
            return curHP > 0.0f;
        }
    }
    public void OnAttack(Vector3 pos, float size, LayerMask enemyMask,float dmg) 
    {

        Collider[] myCols = Physics.OverlapSphere(pos, size, enemyMask);
        foreach (Collider col in myCols)
        {
            IDamage damage = col.GetComponent<IDamage>();
            Vector3 attackVec = col.transform.position - pos;
            attackVec.Normalize();
            if (damage != null) damage.OnDamage(dmg);
        }

    }
    public virtual void OnDamage(float dmg)
    {
        curHP -= dmg;
        if (curHP > 0.0f)
        {
            myAnim.SetTrigger("Hit");
           
        }
        else
        {
            OnDead();
            myAnim.SetTrigger("Die");
        }
    } 
    protected virtual void OnDead()
    {

    }
}
