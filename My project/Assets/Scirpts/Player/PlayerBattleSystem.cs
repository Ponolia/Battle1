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

    public float RemainQSkillCool { get => QSkillInfo.curSkillCool; }
    public float RemainWSkillCool { get => WSkillInfo.curSkillCool; }
    public float RemainESkillCool { get => ESkillInfo.curSkillCool; }
    public float RemainRSkillCool { get => RSkillInfo.curSkillCool; }

}
