using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUISlot : UIProperty<SkillUISlot>
{
    public TMPro.TMP_Text myText = null;

    public Skill mySkill = null;
   
    void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            if (mySkill != null)
            {
                myAllImage[i].sprite = mySkill.Icon;
            }
        }
    }

  
    void Update()
    {
        if (myAllImage[0].sprite != mySkill?.Icon || myAllImage[1].sprite != mySkill?.Icon)
        {
            for (int i = 0; i < 2; i++)
            {
                if (mySkill != null)
                {
                    myAllImage[i].sprite = mySkill.Icon;
                }
            }
        }
    }
    public IEnumerator Cooling(float t)
    {

        if (Mathf.Approximately(myAllImage[1].fillAmount, 1.0f))
        {
            myAllImage[1].raycastTarget = false;
            myAllImage[1].fillAmount = 0.0f;
            float s = 1.0f / t;//�ӵ�
            while (myAllImage[1].fillAmount < 1.0f)
            {
                myAllImage[1].fillAmount += Time.deltaTime * s;
                myText.text = ((1.0f - myAllImage[1].fillAmount) * t).ToString("0");
                yield return null;
            }
            myText.text = string.Empty;
            myAllImage[1].fillAmount = 1.0f;

            myAllImage[1].raycastTarget = true;
        }
    }
}
