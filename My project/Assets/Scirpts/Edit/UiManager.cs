using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Slider myHpSlider;
    public Slider myExpSlider;
    //public Inventory myInventory;
    //public Equipment myEquipment;
    public SKillUI mySkillUI;
    //public SkillWindow mySkillWindow;

    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    public void DefalutSetting()
    {
        mySkillUI.SetSkillUI();
    }

}
