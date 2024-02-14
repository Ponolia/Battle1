using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="Skill",menuName ="SkillObject/Skill",order =int.MaxValue)]
public class Skill : ScriptableObject
{
    [SerializeField]
    public Sprite Icon = null;
    [SerializeField]
    public AnimationClip animationClip = null;
    [SerializeField]
    public GameObject EffectPrefab = null;

    public bool IsAreaSelect = false;
    public GameObject AreaPrefab = null;
    public bool IsDash = false;
    [SerializeField]
    public UnityEvent<float, Transform, LayerMask> damageArea = null;

    //데미지 관련해서는 MultiDamage * 플레이어공격력 + AddDamage 로 계산하여 데미지를 줄 예정
    [SerializeField]
    public float MultiDamage = 1.0f;
    [SerializeField]
    public float AddDamage = 0.0f;


    public float CoolTime = 0.0f;

    public int AttackCount = 1;
    public float AttackInterval = 0.2f;
    public float KnockackDist = 0.0f;

    [SerializeField]
    public AudioClip SkillSound;

    //일단 사용하지 않음.
    [SerializeField]
    public float Range = 1.0f;

    //일단 사용하지 않음.
    [SerializeField]
    public float Duration = 0.0f;
    //============ 각 스킬의 피해 판정 함수 ===========================
    //public void WarriorSkill_1(float dmg, Transform transform, LayerMask enemyMask)
    //{
    //    BattleManager.AttackDirCircle(transform.position + transform.forward * 3.0f, 3.0f,
    //        enemyMask, dmg, transform.forward, false, KnockackDist);
    //}

    //public void WarriorSkill_2(float dmg, Transform transform, LayerMask enemyMask)
    //{
    //    BattleManager.AttackCircle(transform.position, 3.0f, enemyMask, dmg,
    //        false, KnockackDist);
    //}
    //public static void WarriorSkill_3(float dmg, Transform transform, LayerMask enemyMask)
    //{
    //    BattleManager.AttackCircle(transform.position, 1.0f, enemyMask, dmg,
    //        false, 0.1f);
    //}
}
