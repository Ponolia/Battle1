 using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class MoveMent : CharProperty
{
    public float moveSpeed = 1.0f;
    public float rotSpeed = 360.0f;
    public LayerMask skillClickMask;
    public LayerMask virtualGroundMask;

    Coroutine coMove = null;
    Coroutine coRot = null;
    Coroutine coAttack = null;
    private void Update()
    {

    }

    void StopTargetCoroutine(Coroutine target)
    {
        if(target != null)
        {
            StopCoroutine(target);
            target = null;
        }
    }

    public void MovetoPos(Vector3 pos)
    {
        MovetoPos(pos, null);
    }
    public void MovetoPos(Vector3 pos, UnityAction done)
    {
        StopTargetCoroutine(coMove);
        StopTargetCoroutine(coRot);
        StopTargetCoroutine(coAttack);
        coMove = StartCoroutine(MovingPos(pos, done));
    }
    protected IEnumerator MovingPos(Vector3 pos, UnityAction done)
    {
        myAnim.SetBool("IsMove", true);
        Vector3 dir = pos - transform.position;
        float dist = dir.magnitude;
       // dir.y = 0.0f;
        dir.Normalize();

        StopTargetCoroutine(coRot);
        coRot = StartCoroutine(Rotating(dir));
        while (dist > 0.0f)
        {
            if (!myAnim.GetBool("IsAttack"))
            {
                float delta = moveSpeed * Time.deltaTime;

                if (delta > dist) delta = dist;
                dist -= delta;

                transform.Translate(dir * delta, Space.World);
            }
            yield return null;
        }
        myAnim.SetBool("IsMove", false);
        done?.Invoke();
    }
    protected IEnumerator Rotating(Vector3 dir)
    {
        float angle = Vector3.Angle(dir, transform.forward);
        float rotDir = 1.0f;
        if (Vector3.Dot(dir, transform.right) < 0.0f)
        {
            rotDir = -1.0f;
        }

        while (!Mathf.Approximately(angle, 0.0f))
        {
            if (!myAnim.GetBool("IsAttack"))
            {
                float delta = rotSpeed * Time.deltaTime;
                if (delta > angle) delta = angle;
                angle -= delta;
                transform.Rotate(Vector3.up * delta * rotDir);
            }
            yield return null;
        }
    }
    protected void AttackTarget(Transform target)
    {
        //StopAllCoroutines();
        StopTargetCoroutine(coMove);
        StopTargetCoroutine(coRot);
        StopTargetCoroutine(coAttack);
        coAttack = StartCoroutine(Attacking(target));
    }

    IEnumerator Attacking(Transform target)
    {
        bool attacking = false;
        playTime = battleStat.AttackDelay;
        ILive live = target.GetComponent<ILive>();
        while (target != null)
        {
            if (live != null && !live.IsLive) break;
            playTime += Time.deltaTime;
            Vector3 dir = target.position - transform.position;
            float dist = dir.magnitude - battleStat.AttackRange;
            if (dist < 0.1f) dist = 0.0f;
            dir.Normalize();
            
            float delta = moveSpeed * Time.deltaTime;
            
            if (!attacking && !Mathf.Approximately(dist, 0.0f))
            {
                myAnim.SetBool("IsMove", true);
                if (delta > dist) delta = dist;
                if (!myAnim.GetBool("IsAttack"))
                {
                    transform.Translate(dir * delta, Space.World);
                }
                
            }
            else
            {
                attacking = true;
                if (dist > 0.5f) attacking = false;
                else
                {
                    myAnim.SetBool("IsMove", false);
                    if (playTime >= battleStat.AttackDelay)
                    {
                        playTime = 0.0f;
                        myAnim.SetTrigger("Attack");
                    }
                }
            }

            float angle = Vector3.Angle(dir, transform.forward);
            float rotDir = 1.0f;
            if (Vector3.Dot(dir, transform.right) < 0.0f)
            {
                rotDir = -1.0f;
            }
            delta = rotSpeed * Time.deltaTime;
            if (!Mathf.Approximately(angle, 0.0f))
            {
                if (delta > angle) delta = angle;
                transform.Rotate(Vector3.up * delta * rotDir);
            }

            yield return null;
        }
    }
    //protected void StopMove()
    //{
    //    foreach (Coroutine co in moveCoroutineList)
    //    {
    //        if (co != null)
    //        {
    //            StopCoroutine(co);
    //        }
    //    }
    //    moveCoroutineList.Clear();
    //}
}
