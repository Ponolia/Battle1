using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Skills
{
    public Skill QSkill;
    public Skill WSkill;
    public Skill ESkill;
    public Skill RSkill;
}
public class SkillInfo
{
    public Skill skill;
    public float curSkillCool;
    public int skillLV;

    public SkillInfo(Skill skill = null)
    {
        this.skill = skill;
        curSkillCool = 0.0f;
        skillLV = 1;
    }
}

public class PlayerBattleSystem : BattleSystem
{
    public bool CanMove { get; set; } = true;
    protected enum SkillKey
    {
        QSkill,
        WSkill,
        ESkill,
        RSkill
    }

    [SerializeField]
    protected Skills equippedSkills;

    public SkillInfo QSkillInfo;
    public SkillInfo WSkillInfo;
    public SkillInfo ESkillInfo;
    public SkillInfo RSkillInfo;
    public int SkillPoint { get; set; } = 0;

    public float RemainQSkillCool { get => QSkillInfo.curSkillCool; }
    public float RemainWSkillCool { get => WSkillInfo.curSkillCool; }
    public float RemainESkillCool { get => ESkillInfo.curSkillCool; }
    public float RemainRSkillCool { get => RSkillInfo.curSkillCool; }
    protected bool IsSkillAreaSelecting { get; private set; } = false;

    SkillInfo usingSkill = null;
    Vector3 usingSkillPos = Vector3.zero;
    public SkillInfo UsingSkill
    {
        get => usingSkill;
    }
    protected override void Initialize()
    {
        base.Initialize();

        usingSkillPos = transform.position;

        //��ų �ʱ�ȭ
        InitSkill();
    }
    void InitSkill()
    {
        QSkillInfo = new SkillInfo(equippedSkills.QSkill);
        WSkillInfo = new SkillInfo(equippedSkills.WSkill);
        ESkillInfo = new SkillInfo(equippedSkills.ESkill);
        RSkillInfo = new SkillInfo(equippedSkills.RSkill);
    }
    protected void UseSkill(SkillKey skillkey)
    {
        switch (skillkey)
        {
            case SkillKey.QSkill:
                UseSkill(QSkillInfo);
                break;
            case SkillKey.WSkill:
                UseSkill(WSkillInfo);
                break;

            case SkillKey.ESkill:
                UseSkill(ESkillInfo);
                break;

            case SkillKey.RSkill:
                UseSkill(RSkillInfo);
                break;
        }
    }
    private void UseSkill(SkillInfo skillInfo)
    {
        if (skillInfo.skill == null)
        {
            Debug.Log("��ų�� ����� �ʾҽ��ϴ�.");
        }
        if (skillInfo.curSkillCool > 0.0f)
        {
            //Debug.Log($"�ش� ��ų�� ��Ÿ�� �� �Դϴ�. ���� ��Ÿ�� : {skillInfo.curSkillCool}");
            return;
        }

        usingSkill = skillInfo;

        if (skillInfo.skill.IsAreaSelect)
        {
            //skill.AreaPrefab ����
            StartCoroutine(AreaSelecting(skillInfo));
        }
        else
        {
            AnimateSkill(skillInfo, transform.position);
        }

    }
    IEnumerator AreaSelecting(SkillInfo skillInfo)
    {
        yield return null;
        GameObject areaPrefab = skillInfo.skill.AreaPrefab;

        IsSkillAreaSelecting = true;
        GameObject area = Instantiate(areaPrefab, transform.position, Quaternion.identity);
        while (true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, skillClickMask))
            {
                area.transform.position = hit.point;
            }
            //Ű �Է�
            if (Input.GetMouseButtonDown(0))    //����
            {
                Vector3 areaPos = area.transform.position;
                Destroy(area);
                IsSkillAreaSelecting = false;
                AnimateSkill(skillInfo, areaPos);
                break;
            }
            else if (Input.anyKeyDown)          //���
            {
                Destroy(area);
                IsSkillAreaSelecting = false;
                break;
            }

            yield return null;
        }
    }
    void AnimateSkill(SkillInfo skillInfo, Vector3 effectPos)
    {
        //��Ÿ��
        StartCoroutine(CoolingSkill(skillInfo));
        if (!skillInfo.skill)
        {
            usingSkill = skillInfo;            
        }
        usingSkillPos = effectPos;
        myAnim.SetTrigger(skillInfo.skill.animationClip.name);

        myAnim.SetBool("IsAttack", true);
    }
    IEnumerator CoolingSkill(SkillInfo skillInfo)
    {
        skillInfo.curSkillCool = skillInfo.skill.CoolTime;

        GameManager.Inst.UiManager.mySkillUI.CoolTimeSkill(skillInfo);

        while (skillInfo.curSkillCool >= 0.0f)
        {
            skillInfo.curSkillCool -= Time.deltaTime;
            yield return null;
        }
    }

    public void OnSkillEffectStart()
    {
        Vector3 pos = usingSkillPos;
        if (!usingSkill.skill.IsAreaSelect)
            pos = transform.position;

        if (usingSkill.skill.EffectPrefab != null)
            Instantiate(usingSkill.skill.EffectPrefab, pos, transform.rotation);

        //if (UsingSkill != null)
        //    GameManager.Inst.SoundManager.OnSkillEffectSound(UsingSkill);
    }
    public void OnSkillAttack()
    {
        if (usingSkill != null)
        {
            //usingSkill.SkillAttack(curAttackPoint, transform, enemyMask);
            StartCoroutine(SkillAttackCoroutine(usingSkill));
        }
    }
    IEnumerator SkillAttackCoroutine(SkillInfo usingSkill)
    {
        var t = new WaitForSeconds(usingSkill.skill.AttackInterval);
        for (int i = 0; i < usingSkill.skill.AttackCount; i++)
        {
            usingSkill.skill.SkillAttack(curAttackPoint, UsingSkill.skillLV, transform, enemyMask);
            yield return t;
        }
    }
    public override void OnDamage(float dmg, Vector3 attackVec, float knockBackDist, bool isDown)
    {
       
            base.OnDamage(dmg, attackVec, knockBackDist, isDown);
            if (!IsLive)
            {
                //Game Over
                PlayerDead();
                GameManager.Inst.GameOver();
                
            }
        
    }
    public GameObject levelUpEffect;
    public void LevelUp()
    {
        levelUpEffect.SetActive(true);

        battleStat.LV++;
        battleStat.MaxExp += 100;
        curExp = 0;
        battleStat.MaxHpPoint += 20;
        curHP += 20;
        battleStat.MaxMpPoint += 20;
        curMP += 20;
        curAttackPoint += 3;
        curDefensePoint += 3;

        SkillPoint += 3;
        GameManager.Inst.UiManager.mySkillWindow.ChangeInfo();
    }
    void PlayerDead()
    {
        CanMove = false;
        myAnim.SetTrigger("Die");
        myAnim.SetBool("IsImmunity", true);
    }

    public void PlayerRespawn()
    {
        curHP = battleStat.MaxHpPoint;
        CanMove = true;
        myAnim.Play("Idle");
        myAnim.SetBool("IsImmunity", false);
    }
}
