using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleManager : MonoBehaviour
{
    static Vector3 originPos = Vector3.zero;
    static float originSize = 0f;
    public static void AttackDirCircle(Vector3 pos, float size, LayerMask enemyMask, float dmg,
        Vector3 attackVec, bool isDown = false, float knockBackDist = 0)
    {
        Collider[] myCols = Physics.OverlapSphere(pos, size, enemyMask);
        foreach (Collider col in myCols)
        {
            IDamage damage = col.GetComponent<IDamage>();
            if (damage != null) damage.OnDamage(dmg);//, attackVec, knockBackDist, isDown
        }
        //Gizmo test¿ë
        originPos = pos;
        originSize = size;
    }
    public static void AttackCircle(Vector3 pos, float size, LayerMask enemyMask, float dmg,
      bool isDown = false, float knockBackDist = 0)
    {
        Collider[] myCols = Physics.OverlapSphere(pos, size, enemyMask);
        foreach (Collider col in myCols)
        {
            IDamage damage = col.GetComponent<IDamage>();
            Vector3 attackVec = col.transform.position - pos;
            attackVec.y = 0f;
            attackVec.Normalize();
            if (damage != null) damage.OnDamage(dmg);//, attackVec, knockBackDist, isDown
        }
        //Gizmo test¿ë
        originPos = pos;
        originSize = size;
    }
}
