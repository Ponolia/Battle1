using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimEvent : MonoBehaviour
{
    public UnityEvent AttackAct;
    public UnityEvent AttackAct2;
    public UnityEvent AttackAct3;
    public UnityEvent AttackAct4;
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
    public void OnAttack2()
    {
        AttackAct2?.Invoke();
    }
    public void OnAttack3()
    {
        AttackAct3?.Invoke();
    }
    public void OnAttack4()
    {
        AttackAct4?.Invoke();
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
