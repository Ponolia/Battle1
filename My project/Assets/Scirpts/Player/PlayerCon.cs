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
    //public void OnBaseAttack(Vector3 pos, float size, LayerMask enemyMask, float dmg)
    //{
    //    Collider[] myCols = Physics.OverlapSphere(pos, size, enemyMask);
    //    foreach (Collider col in myCols)
    //    {
    //        IDamage damage = col.GetComponent<IDamage>();
    //        Vector3 attackVec = col.transform.position - pos;
    //        attackVec.Normalize();
    //        if (damage != null) damage.OnDamage(dmg);
    //    }
    //}
 
 
}
