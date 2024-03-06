using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMoveMent : BattleSystem
{
    AIPerception _perception = null;
    protected AIPerception myPerception
    {
        get
        {
            if (_perception == null)
            {
                _perception = GetComponent<AIPerception>();
                if (_perception == null)
                {
                    _perception = GetComponentInChildren<AIPerception>();
                }
            }
            return _perception;
        }
    }
    //protected void AttackTargeting(Transform target)
    //{
    //    StopAllCoroutines();
    //    if (target != null)
    //    {
    //        StartCoroutine(Attacking(target));
    //    }

    //}
    //IEnumerator Attacking(Transform target)
    //{
    //    ILive live = target.GetComponent<ILive>();
    //    while (true)
    //    {
    //        if (live != null && !live.IsLive) break;
    //        playTime += Time.deltaTime;
    //        Vector3 dir = target.position - transform.position;
    //        float dist = dir.magnitude - battleStat.AttackRange;
    //        if (dist < 0.2f) dist = 0.0f;
    //        dir.Normalize();

    //        myAnim.SetBool("IsMove", false);
    //        float delta = moveSpeed * Time.deltaTime;
    //        if (!Mathf.Approximately(dist, 0.0f))
    //        {
    //            myAnim.SetBool("IsMove", true);
    //            if (delta > dist) delta = dist;
    //            if (!myAnim.GetBool("IsAttack"))
    //            {
    //                transform.Translate(dir * delta, Space.World);
    //            }
    //        }
    //        else
    //        {

    //            if (playTime >= battleStat.AttackDelay)
    //            {
    //                playTime = 0.0f;
    //                myAnim.SetTrigger("Attack");
    //                // RndAttack();
    //            }

    //        }
    //        // È¸Àü
    //        float angle = Vector3.Angle(dir, transform.forward);
    //        float rotDir = 1.0f;

    //        if (Vector3.Dot(dir, transform.right) < 0.0f)
    //        {
    //            rotDir = -1.0f;
    //        }
    //        delta = rotSpeed * Time.deltaTime;
    //        if (!Mathf.Approximately(angle, 0.0f))
    //        {
    //            if (delta > angle) delta = angle;
    //            transform.Rotate(Vector3.up * delta * rotDir);
    //        }
    //        yield return null;
    //    }
    //}
    //void RndAttack()
    //{
    //    int rndValue = Random.Range(1, 5);
    //    switch (rndValue)
    //    {
    //        case 1:
    //            myAnim.SetTrigger("Attack");
    //            break;
    //        case 2:
    //            myAnim.SetTrigger("Attack2");
    //            break;
    //        default:
    //            break;
    //    }
    //}
}
