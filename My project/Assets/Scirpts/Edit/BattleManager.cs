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
            if (damage != null) damage.OnDamage(dmg, attackVec, knockBackDist, isDown);
        }
        //Gizmo test용
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
            if (damage != null) damage.OnDamage(dmg, attackVec, knockBackDist, isDown);
        }
        //Gizmo test용
        originPos = pos;
        originSize = size;
    }
    //Test용 Gizmo
    private void OnDrawGizmos()
    {
        Color color = Color.blue;
        color.a = 0.5f;
        Gizmos.color = color;
        Gizmos.DrawSphere(originPos, originSize);
    }
    //데미지 표시
    static GameObject dmgPopupPrefab = null;
    public static GameObject DmgPopupPrefab
    {
        get
        {
            if (dmgPopupPrefab == null)
                dmgPopupPrefab = Resources.Load<GameObject>("UI\\DmgPopup");
            return dmgPopupPrefab;
        }
    }
    static GameObject dynamicCanvas = null;
    public static GameObject DynamicCanvas
    {
        get
        {
            if (dynamicCanvas == null)
                dynamicCanvas = GameObject.Find("DynamicCanvas");
            return dynamicCanvas;
        }
    }
    public static void DamagePopup(Transform transform, float dmg)
    {
        GameObject obj = Instantiate(DmgPopupPrefab, DynamicCanvas.transform.GetChild(0));
        obj.GetComponent<TextMeshProUGUI>().text = dmg.ToString();
        obj.GetComponent<DamagePopup>().SetPos(transform);
    }
}
