using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimEvent : MonoBehaviour
{
    public UnityEvent AttackAct;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    public void OnAttack()
    {
        AttackAct?.Invoke();
    }
}
