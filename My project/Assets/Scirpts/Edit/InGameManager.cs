using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    PlayerCon _player;
    /// <summary>
    /// 인게임에서의 플레이어
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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
