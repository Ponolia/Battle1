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
    
    [SerializeField]
    public UnityEvent<float, Transform, LayerMask> damageArea = null;

    //������ �����ؼ��� MultiDamage * �÷��̾���ݷ� + AddDamage �� ����Ͽ� �������� �� ����
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

    //�ϴ� ������� ����.
    [SerializeField]
    public float Range = 1.0f;

    //�ϴ� ������� ����.
    [SerializeField]
    public float Duration = 0.0f;
    PlayerCon player;
    public void SkillAttack(float Dmg,int skillLV, Transform tr, LayerMask enemyMask)
    {
        damageArea?.Invoke(TotalDamage(Dmg, skillLV), tr, enemyMask);
    }
    public float TotalDamage(float playerAttackPoint, int skillLV)
    {
        return Mathf.Round((MultiDamage * playerAttackPoint + AddDamage) * (0.1f * skillLV + 1.0f));
    }
   // ============ �� ��ų�� ���� ���� �Լ� ===========================
    public void SwordSkill_1(float dmg, Transform transform, LayerMask enemyMask)
    {
        BattleManager.AttackDirCircle(transform.position, 2.0f, enemyMask, dmg
           ,transform.forward, false, KnockackDist);
    }
    public void SwordSkill_2(float dmg, Transform transform, LayerMask enemyMask)
    {
        BattleManager.AttackDirCircle(transform.position + transform.forward, 3.0f,
            enemyMask, dmg, transform.forward, false, KnockackDist);
    }
    public  void SwordSkill_3(float dmg, Transform transform, LayerMask enemyMask)
    {
        BattleManager.AttackDirCircle(transform.position, 3.0f,
           enemyMask, dmg, transform.forward, false, KnockackDist);
    }
    public static void SwordSkill_4(float dmg, Transform transform, LayerMask enemyMask)
    {
        BattleManager.AttackDirCircle(transform.position, 2.0f, enemyMask, dmg,
           transform.forward, false );
    }
}
