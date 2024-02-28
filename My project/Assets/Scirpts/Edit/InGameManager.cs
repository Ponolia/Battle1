using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    [HideInInspector]
    public SaveData[] saveDatas = new SaveData[3];
   // QuestDataObject[] questDataObj = new QuestDataObject[3];

    public InventoryData[] InventoryDatas = new InventoryData[20];
    public EquipmentData[] EquipmentDatas = new EquipmentData[4];
    public ConsumptionItemData[] ConsumptionItemDatas = new ConsumptionItemData[3];
    PlayerCon _player;
    /// <summary>
    /// �ΰ��ӿ����� �÷��̾�
    /// </summary>
    public PlayerCon myPlayer
    {
        get
        {
            if (_player == null)
                _player = FindObjectOfType<PlayerCon>();
            return _player;
        }
        set { _player = value; }
    }
    public Skills myPlayerSkill
    {
        get
        {
            return myPlayer.GetSkill();
        }
    }
    PlayerInfo _playerInfo;
    public int curSaveSlotNum = -1;
    public uint Gold
    {
        get => _playerInfo.Gold;
        set
        {
            _playerInfo.Gold = value;
        }
    }
    //���̺� �ε� ���
    public void Save()
    {
        SaveData _saveData = saveDatas[curSaveSlotNum];
        //PlayerInfo _playerInfo = new PlayerInfo();
        PlayerCon player = GameManager.Inst.inGameManager.myPlayer;
        _playerInfo.SceneNum = GameManager.Inst.curSceneNum;
        _playerInfo.Pos = player.transform.position;
        _playerInfo.BattleStat = player.battleStat;
        _playerInfo.CurHP = player.curHP;
        _playerInfo.CurMP = player.curMP;
        _playerInfo.CurAttackPoint = player.curAttackPoint;
        _playerInfo.CurDefencePoint = player.curDefensePoint;
        _playerInfo.CurExp = player.curExp;

        _playerInfo.PlayTime = 0;

        _playerInfo.Gold = Gold;

        _playerInfo.SkillPoint = player.SkillPoint;
        _playerInfo.QSkillLV = player.QSkillInfo.skillLV;
        _playerInfo.WSkillLV = player.WSkillInfo.skillLV;
        _playerInfo.ESkillLV = player.ESkillInfo.skillLV;

        InventoryDatas = GameManager.Inst.UiManager.myInventory.GetInventoryData();
        for (int i = 0; i < 20; i++)
        {
            _saveData.InventoryDatas[i] = InventoryDatas[i];
        }
        EquipmentDatas = GameManager.Inst.UiManager.myEquipment.GetEquipmentData();
        for (int i = 0; i < 4; i++)
        {
            _saveData.EquipmentDatas[i] = EquipmentDatas[i];
        }
        ConsumptionItemDatas = GameManager.Inst.UiManager.myConsumptionItem.GetConsumptionItemData();
        for (int i = 0; i < 3; i++)
        {
            _saveData.ConsumptionItemDatas[i] = ConsumptionItemDatas[i];
        }
        //����
        //curSaveSlotNum = GameManager.Inst.curSceneNum;
        saveDatas[curSaveSlotNum].IsEmpty = false;
        saveDatas[curSaveSlotNum].playerInfo = _playerInfo;

        ////����Ʈ����
        //for (int i = 0; i < saveDatas[curSaveSlotNum].serializedQuestDatas.Length; ++i)
        //{
        //    saveDatas[curSaveSlotNum].serializedQuestDatas[i].questID = questDataObj[curSaveSlotNum].questObjects[i].data.id;
        //    saveDatas[curSaveSlotNum].serializedQuestDatas[i].completeCount
        //        = questDataObj[curSaveSlotNum].questObjects[i].data.completeCount;
        //    saveDatas[curSaveSlotNum].serializedQuestDatas[i].status
        //        = questDataObj[curSaveSlotNum].questObjects[i].status;
        //}

        FileManager.SaveBinary($"saveData{curSaveSlotNum}.sav", new SerializedSaveData(saveDatas[curSaveSlotNum]));

        Debug.Log($"{curSaveSlotNum}�� ���� �����.");
    }
    public void Load(PlayerCon player)
    {
        //scriptable object���� _playerInfo�� ���� ��������
        SaveData _saveData = saveDatas[curSaveSlotNum];
        _playerInfo = _saveData.playerInfo;

        //player ���� �ֱ�
        if (!saveDatas[curSaveSlotNum].IsEmpty)
        {
            GameManager.Inst.UiManager.myInventory.SetInventoryData(_saveData.InventoryDatas);
            GameManager.Inst.UiManager.myEquipment.SetEquipmentData(_saveData.EquipmentDatas);
            GameManager.Inst.UiManager.myConsumptionItem.SetConsumptionItemData(_saveData.ConsumptionItemDatas);

            player.transform.position = _playerInfo.Pos;
            player.battleStat = _playerInfo.BattleStat;
            player.curHP = _playerInfo.CurHP;
            player.curMP = _playerInfo.CurMP;
            player.curAttackPoint = _playerInfo.CurAttackPoint;
            player.curDefensePoint = _playerInfo.CurDefencePoint;
            player.curExp = _playerInfo.CurExp;
            player.SkillPoint = _playerInfo.SkillPoint;
            player.QSkillInfo.skillLV = _playerInfo.QSkillLV;
            player.WSkillInfo.skillLV = _playerInfo.WSkillLV;
            player.ESkillInfo.skillLV = _playerInfo.ESkillLV;

            Gold = _playerInfo.Gold;
            GameManager.Inst.UiManager.myGoodsUI.ChangeCoin(_playerInfo.Gold);
        }
        else
        {
            Gold = 0;
        }
    }
}
public struct PlayerInfo
{
    //�� (��� �ʿ� �־�����)
    public int SceneNum;
    public Vector3 Pos;


    //��� ����
    public uint Gold;

    public float CurHP;
    public float CurMP;
    public float CurAttackPoint;
    public float CurDefencePoint;
    public float CurExp;
    public BattleStat BattleStat;

    public int SkillPoint;
    public int QSkillLV;
    public int WSkillLV;
    public int ESkillLV;

    //�÷��� �ð�?
    public ulong PlayTime;
}
