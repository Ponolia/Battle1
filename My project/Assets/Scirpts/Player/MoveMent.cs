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

    List<Coroutine> moveCoroutineList = new List<Coroutine>();

    private void Update()
    {

    }
    public void MovetoPos(Vector3 pos)
    {
        MovetoPos(pos, null);
    }
    public void MovetoPos(Vector3 pos, UnityAction done)
    {
        StopAllCoroutines();
        StartCoroutine(MovingPos(pos, done));
    }
    protected IEnumerator MovingPos(Vector3 pos, UnityAction done)
    {
        myAnim.SetBool("IsMove", true);
        Vector3 dir = pos - transform.position;
        float dist = dir.magnitude;
       // dir.y = 0.0f;
        dir.Normalize();

        moveCoroutineList.Add( StartCoroutine(Rotating(dir)));
        while (dist > 0.1f)
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
        dir.y = 0;
        float angle = Vector3.Angle(dir, transform.forward);
        float rotDir = 1.0f;
        if (Vector3.Dot(dir, transform.right) < 0.0f)
        {
            rotDir = -1.0f;
        }//왼쪽 클릭 했는지 확인

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
        StopAllCoroutines();
        StartCoroutine(Attacking(target));
    }

    IEnumerator Attacking(Transform target)
    {
        ILive live = target.GetComponent<ILive>();
        while (target != null)
        {
            if (live != null && !live.IsLive) break;
            playTime += Time.deltaTime;
            Vector3 dir = target.position - transform.position;
            float dist = dir.magnitude - battleStat.AttackRange;
            if (dist < 0.2f) dist = 0.0f;
            dir.Normalize();
            
            float delta = moveSpeed * Time.deltaTime;
            
            if (!Mathf.Approximately(dist, 0.0f))
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
                myAnim.SetBool("IsMove", false);
                if (playTime >= battleStat.AttackDelay)
                {
                    playTime = 0.0f;
                    myAnim.SetTrigger("Attack");
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
