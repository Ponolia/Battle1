using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerCon : PlayerBattleSystem
{
    public Transform myWeaponPos;
    public LayerMask attackMask;
    public UnityEvent<Transform> attackAct;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        // 기본 공격
        if (Input.GetMouseButtonDown(0) && !myAnim.GetBool("IsAttack")
                    && !EventSystem.current.IsPointerOverGameObject())
        {
           
            myAnim.SetBool("Attack", true);
        }
        // 스킬 애니
        if (Input.GetKeyDown(KeyCode.Q) && !myAnim.GetBool("IsAttack"))
        {
            QSkill();
        }
        if (Input.GetKeyDown(KeyCode.W) && !myAnim.GetBool("IsAttack"))
        {
            WSkill();
        }
        if (Input.GetKeyDown(KeyCode.E) && !myAnim.GetBool("IsAttack"))
        {
            ESkill();
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
    public void QSkill()
    {
        myAnim.SetTrigger("Skill1");
    }
    public void WSkill()
    {
        myAnim.SetTrigger("Skill2");
    }
    public void ESkill()
    {
        myAnim.SetTrigger("Skill3");
    }
   
    public void OnSkill()
    {
        Collider[] list = Physics.OverlapSphere(myWeaponPos.position, 2.0f, enemyMask);
        foreach (Collider col in list)
        {
            IDamage damage = col.GetComponent<IDamage>();
            if (damage != null) damage.OnDamage(100.0f);
        }
    }
    protected override void OnDead()
    {
        myAnim.SetTrigger("Hit");
    }
}
