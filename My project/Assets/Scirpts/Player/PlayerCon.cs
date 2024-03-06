using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerCon : PlayerBattleSystem
{
    public Item PickUpItem = null;

    public LayerMask attackMask;
    public UnityEvent<Transform> attackAct;

    public GameObject[] BodyObject = null;
    public GameObject[] ArmorsObject = null;
    public GameObject[] WeaponsObject = null;

    public GameObject BootsObject = null;
    public GameObject HelmetObject = null;

    GameObject destinationMarker;

    void Start()
    {
       // destinationMarker = Resources.Load<GameObject>("destinationMarker");
        Initialize();

        //GameObject miniMapIcon = Instantiate(Resources.Load<GameObject>("UI\\MiniMapIcon"),
        //  GameManager.Inst.UiManager.myMiniMapIcons);
        //miniMapIcon.GetComponent<MiniMapIcon>().SetTarget(transform, Color.green);
    }

    void Update()
    {
        if (CanMove)
        {
            // 스킬 애니
            if (Input.GetKeyDown(KeyCode.Q) && !myAnim.GetBool("IsAttack"))
            {
                UseSkill(SkillKey.QSkill);
            }
            if (Input.GetKeyDown(KeyCode.W) && !myAnim.GetBool("IsAttack"))
            {
                UseSkill(SkillKey.WSkill);
            }
            if (Input.GetKeyDown(KeyCode.E) && !myAnim.GetBool("IsAttack"))
            {
                UseSkill(SkillKey.ESkill);
            }
            if (Input.GetKeyDown(KeyCode.R) && !myAnim.GetBool("IsAttack"))
            {
                UseSkill(SkillKey.RSkill);
            }
        }
    }
    public void SetTarget(Transform target)
    {
        myTarget = target;
        AttackTarget(myTarget);
    }
    protected override void OnDead()
    {
        myAnim.SetTrigger("Die");
    }
    public Skills GetSkill()
    {
        return equippedSkills;
    }

    // 상점, 맵이동 오브젝트 클릭
    public void OnActiveObj(Transform target)
    {
        target.GetComponent<Teleport>().Enter(this);
    }


    public void OnEquipItem(Item EquipmentItem)
    {
        if (EquipmentItem != null)
        {
            if (EquipmentItem.EquipmentType == Item.EQUIPMENTTYPE.Weapon)
            {
                curAttackPoint += EquipmentItem.StatPoint;
            }
            else
            {
                curDefensePoint += EquipmentItem.StatPoint;
            }
        }
        EquipmentOvjectActivate(EquipmentItem);
    }
    public void OnUnmountITem(Item EquipmentItem)
    {
        if (EquipmentItem != null)
        {
            if (EquipmentItem.EquipmentType == Item.EQUIPMENTTYPE.Weapon)
            {
                curAttackPoint -= EquipmentItem.StatPoint;
            }
            else
            {
                curDefensePoint -= EquipmentItem.StatPoint;
            }
        }
        EquipmentOvjectDisabled(EquipmentItem);
    }

    public void EquipmentOvjectActivate(Item item)
    {
        switch (item.EquipmentType)
        {
            case Item.EQUIPMENTTYPE.Armor:
                for (int i = 0; i < BodyObject.Length - 2; i++)
                {
                    BodyObject[i].SetActive(false);
                }
                foreach (GameObject Body in ArmorsObject)
                {
                    Body.SetActive(true);
                }
                break;

            case Item.EQUIPMENTTYPE.Boots:
                BootsObject.SetActive(true);
                BodyObject[BodyObject.Length - 1].SetActive(false);
                break;

            case Item.EQUIPMENTTYPE.Helmet:
                HelmetObject.SetActive(true);
                BodyObject[BodyObject.Length - 2].SetActive(false);
                break;

            case Item.EQUIPMENTTYPE.Weapon:
                for (int i = 0; i < WeaponsObject.Length; i++)
                {
                    if (i == item.WeaponID)
                    {
                        WeaponsObject[i].SetActive(true);
                    }
                    else
                    {
                        WeaponsObject[i].SetActive(false);
                    }
                }
                break;
        }
    }
    public void EquipmentOvjectDisabled(Item item)
    {
        switch (item.EquipmentType)
        {
            case Item.EQUIPMENTTYPE.Armor:
                for (int i = 0; i < BodyObject.Length - 2; i++)
                {
                    BodyObject[i].SetActive(true);
                }
                foreach (GameObject Body in ArmorsObject)
                {
                    Body.SetActive(false);
                }
                break;
            case Item.EQUIPMENTTYPE.Boots:
                BootsObject.SetActive(false);
                BodyObject[BodyObject.Length - 1].SetActive(true);
                break;
            case Item.EQUIPMENTTYPE.Helmet:
                HelmetObject.SetActive(false);
                BodyObject[BodyObject.Length - 2].SetActive(true);
                break;
            case Item.EQUIPMENTTYPE.Weapon:
                for (int i = 0; i < WeaponsObject.Length; i++)
                {
                    if (i != 0)
                    {
                        WeaponsObject[i].SetActive(false);
                    }
                    else
                    {
                        WeaponsObject[i].SetActive(true);
                    }
                }
                break;
        }
    }
    public void OnUsePotion(Item Item)
    {
        if (Item != null)
        {
            if (Item.PotionType == Item.POTIONTYPE.Hp)
            {
                curHP = Mathf.Min(curHP + Item.StatPoint, battleStat.MaxHpPoint);
            }
            else
            {
                curMP += Item.StatPoint;
            }
        }
    }
    public void SetStatus(TMPro.TMP_Text[] statList)
    {
        statList[0].text = battleStat.LV.ToString();
        statList[1].text = battleStat.MaxHpPoint.ToString();
        statList[2].text = battleStat.MaxMpPoint.ToString();
        statList[3].text = curAttackPoint.ToString();
        statList[4].text = curDefensePoint.ToString();
    }
}
