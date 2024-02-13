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
    public bool IsLive
    {
        get
        {
            return curHP > 0.0f;
        }
    }
    public void OnAttack()
    {
        if (IsLive)
        {
            IDamage damage = myTarget.GetComponent<IDamage>();
            if (damage != null) damage.OnDamage(battleStat.AttackPoint);
        }
    }
    public virtual void OnDamage(float dmg)
    {
        curHP -= dmg;
        if (curHP > 0.0f)
        {
            myAnim.SetTrigger("Hit");
            // StartCoroutine(DamagingEff(0.3f));
        }
    } 
}
