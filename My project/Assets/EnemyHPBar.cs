using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPBar : MonoBehaviour
{
    public Transform myTaeget;
   
   
    void Update()
    {
        if (myTaeget != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(myTaeget.position + new Vector3(0, 2.0f, 0));
        }
    }
    public void SetTarget(Transform target)
    {
        myTaeget= target;
    }
}
