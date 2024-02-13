using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCon : BattleSystem
{
    
    // Start is called before the first frame update
    void Start()
    {
        curHP = battleStat.MaxHpPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            myAnim.SetTrigger("Attack");
        }
        
    }
    public void ComboCeckStart()
    {

    }
   public void SetTarget(Transform target)
    {
        myTarget = target;
        AttackTarget(myTarget);
    }
    public void Dead()
    {
        if (curHP <= 0.0f)
        {
            myAnim.SetTrigger("Die");
        }
    }
}
