using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharProperty : MonoBehaviour
{
    Animator anim = null;
    protected Animator myAnim
    {
        get
        {
            if (anim == null)
            {
                anim = GetComponent<Animator>();
                if (anim == null)
                {
                    anim = GetComponentInChildren<Animator>();
                }
            }
            return anim;
        }
    }
    Collider _col = null;
    protected Collider myCol
    {
        get
        {
            if (_col == null)
            {
                _col = GetComponent<Collider>();
            }
            return _col;
        }
    }
    public BattleStat battleStat;
    public Slider myHpBar = null;
    float _hp = 0.0f;
    protected float curHP
    {
        get => _hp;
        set
        {
            _hp = Mathf.Clamp(value, 0.0f, battleStat.MaxHpPoint);
            if (myHpBar != null) myHpBar.value = _hp / battleStat.MaxHpPoint;
        }
    }
    protected float playTime = 0.0f;
}
