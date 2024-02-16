using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IDamage
{
    void OnDamage(float damage);
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
    [SerializeField] protected LayerMask enemyMask;
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
    public virtual void OnAttack() 
    {
        IDamage damage = myTarget.GetComponent<IDamage>();
        if (damage != null) damage.OnDamage(battleStat.AttackPoint);
        //Collider[] myCols = Physics.OverlapSphere(pos, size, enemyMask);
        //foreach (Collider col in myCols)
        //{
        //    IDamage damage = col.GetComponent<IDamage>();
        //    Vector3 attackVec = col.transform.position - pos;
        //    attackVec.Normalize();
        //    if (damage != null) damage.OnDamage(dmg);
        //}

    }
    public virtual void OnDamage(float dmg)
    {
        curHP -= dmg;
        if (curHP > 0.0f)
        {
            myAnim.SetTrigger("Hit");
            StartCoroutine(DamagingEff(0.3f));
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
    IEnumerator DamagingEff(float t)
    {
        foreach (Renderer render in myAllRenders)
        {
            render.material.color = Color.red;
        }

        yield return new WaitForSeconds(t);

        foreach (Renderer render in myAllRenders)
        {
            render.material.color = Color.white;
        }
    }
}
