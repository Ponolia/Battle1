using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Slider myHpSlider;
    public Slider myMpSlider;
    public Slider myExpSlider;

    public Inventory myInventory;
    public Equipment myEquipment;
    public ConsumptionItem myConsumptionItem;
    public SKillUI mySkillUI;
    public SkillWindow mySkillWindow;
    public GoodsUI myGoodsUI;
    public GameObject myGameOverWindow;
    public Transform myMiniMapIcons;
    public GameObject myBossHpBar;
   // public QuestListUI myQuestListUI;
    public LevelUpText myLevelUpText;


    public void DefalutSetting()
    {
        mySkillUI.SetSkillUI();
    }
    public void OnGotoTitleButton()
    {
        GameManager.Inst.GotoTitle();
    }

    public void OnPlayerRespawn()
    {
        GameManager.Inst.PlayerRespawn();
    }

    public void OnPlayerRespawnAndGoTitle()
    {
        GameManager.Inst.PlayerRespawn();
        GameManager.Inst.GotoTitle();
    }

}
