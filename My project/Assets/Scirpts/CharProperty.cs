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
    Renderer _renderer = null;
    protected Renderer myRenderer
    {
        get
        {
            if (_renderer == null)
            {
                _renderer = GetComponent<Renderer>();
                if (_renderer == null)
                {
                    _renderer = GetComponentInChildren<Renderer>();
                }
            }
            return _renderer;
        }
    }
    Renderer[] _allRenders = null;
    protected Renderer[] myAllRenders
    {
        get
        {
            if (_allRenders == null)
            {
                _allRenders = GetComponentsInChildren<Renderer>();
            }
            return _allRenders;
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
    public Slider myMpBar = null;
    public Slider myExpBar = null;
    float _hp = 0.0f;
    public float curHP
    {
        get => _hp;
        set
        {
            _hp = Mathf.Clamp(value, 0.0f, battleStat.MaxHpPoint);
            if (myHpBar != null) myHpBar.value = _hp / battleStat.MaxHpPoint;
        }
    }
    float _mp = 0.0f;

    public float curMP
    {
        get => _mp;
        set
        {
            _mp = Mathf.Clamp(value, 0.0f, battleStat.MaxMpPoint);
        }
    }
    float _exp = 0.0f;

    public float curExp
    {
        get => _exp;
        set
        {
            _exp = Mathf.Clamp(value, 0.0f, battleStat.MaxExp);
            if (myExpBar != null) myExpBar.value = _exp / battleStat.MaxExp;
        }
    }
    float _ap = 0.0f;
    public float curAttackPoint
    {
        get => _ap;
        set
        {
            _ap = value;
        }
    }

    float _dp = 0.0f;

    public float curDefensePoint
    {
        get => _dp;
        set
        {
            _dp = value;
        }
    }
    protected float playTime = 0.0f;
}
