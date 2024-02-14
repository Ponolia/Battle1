using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCon : PlayerBattleSystem
{
    public Transform myWeaponPos;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !myAnim.GetBool("IsAttack"))
        {
            myAnim.SetTrigger("Skill1");
        }
        if (Input.GetKeyDown(KeyCode.W) && !myAnim.GetBool("IsAttack"))
        {
            myAnim.SetTrigger("Skill2");
        }
        if (Input.GetKeyDown(KeyCode.E) && !myAnim.GetBool("IsAttack"))
        {
            myAnim.SetTrigger("Skill3");
        }
        if (Input.GetKeyDown(KeyCode.R) && !myAnim.GetBool("IsAttack"))
        {

        }
    }
   public void SetTarget(Transform target)
    {
        myTarget = target;
        AttackTarget(myTarget);
    }
    protected override void OnDead()
    {
        
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,battleStat.AttackRange);
    }
}
