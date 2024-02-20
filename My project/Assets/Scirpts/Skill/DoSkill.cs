using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoSkill : MonoBehaviour
{
    public Skill skill;
    public PlayerCon player;
    public Image Icon;
    public Image CoolTime;

    private void Start()
    {
        Icon.sprite = skill.Icon;
        CoolTime.fillAmount = 0;
    }
    public void OnClicked()
    {
        if (CoolTime.fillAmount > 0) return;
        StartCoroutine(cool);
    }
    IEnumerator cool()
    {
        float t = 1f / skill.CoolTime;
        float f = 0;
        CoolTime.fillAmount = 1;

        while (CoolTime.fillAmount > 0)
        {
            CoolTime.fillAmount = Mathf.Lerp(1, 0, f);
            f += (Time.deltaTime * t);
            yield return null;
        }
    }
}
