using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimEvent : MonoBehaviour
{
    public UnityEvent AttackAct;
    public UnityEvent DeadAct;
    public UnityEvent SkillAct;
    public UnityEvent SkillEffectStartAction;

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
    public void OnDead()
    {
        DeadAct?.Invoke();
    }
    public void OnSkill()
    {
        SkillAct?.Invoke();
    }
    public void OnSkillEffectStart()
    {
        SkillEffectStartAction?.Invoke();
    }

}
