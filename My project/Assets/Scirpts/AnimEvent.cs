using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimEvent : MonoBehaviour
{
    public UnityEvent AttackAct;
    public UnityEvent ComboStart;
    public UnityEvent ComboEnd;
    public UnityEvent DeadAct;
    public UnityEvent SkillAct;

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
    public void ComboCeckStart()
    {
        ComboStart?.Invoke();
    }
    public void ComboCeckEnd()
    {
        ComboEnd?.Invoke();
    }
    public void OnDead()
    {
        DeadAct?.Invoke();
    }
    public void OnSkill()
    {
        SkillAct?.Invoke();
    }
}
