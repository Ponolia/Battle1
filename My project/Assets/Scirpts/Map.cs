using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Map : CharProperty
{
    public Transform target;
    public LayerMask enemyMask;
    public UnityEvent findEnemy;
    public UnityEvent lostEnemy;
    public void Down()
    { 
        curHP = 0.0f;
        target.GetComponentInChildren<Animator>().SetTrigger("Die");
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((enemyMask & 1 << other.gameObject.layer) != 0)
        {
            target = other.transform;
            findEnemy?.Invoke();
            Invoke("Down", 10.0f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (target == other.transform)
        {
            target = null;
            lostEnemy?.Invoke();
        }
    }
}
