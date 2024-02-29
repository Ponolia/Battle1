using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSlot : MonoBehaviour
{
    public SaveData saveData;
    public TMPro.TextMeshProUGUI tmp;
    public GameObject deleteButton;

    private void Awake()
    {
        UpdateSlot();
    }

    public void DeleteLoadSlot()
    {
        saveData.IsEmpty = true;
        saveData.playerInfo = default;
        UpdateSlot();
    }

    /// <summary>
    /// SaveData�� �ٲ�ų� ó���� �ѹ� ����.
    /// SaveData�� �����ͼ� ȭ�鿡 ������.
    /// </summary>
    void UpdateSlot()
    {
        if (saveData.IsEmpty)
        {
            tmp.text = "Empty";
            deleteButton.SetActive(false);
        }
        else
        {
            tmp.text = $"LV.{saveData.playerInfo.BattleStat.LV}";
            deleteButton.SetActive(true);
        }
    }
}
