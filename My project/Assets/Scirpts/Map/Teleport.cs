using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Teleport : MonoBehaviour
{
    public Button btn1, btn2, btn3;
    public RectTransform uiGroup;
    public Animator anim;
    public TMPro.TMP_Text talkText;
    
    PlayerCon enterPlayer;

    bool isActive;

    public void OnButton()
    {        
        enterPlayer.transform.position = new Vector3(Random.Range(-10f, 10f),0,0);
        Exit();
    }
    public void OnClick()
    {
        enterPlayer.transform.position = new Vector3(0.0f,0.0f,Random.Range(-1.0f,10.0f));
        Exit();
    }
    public void OnEscape()
    {
        // 아이템을 가지고 있다면? 가능!
        enterPlayer.transform.position = new Vector3(20, 0, 0);
        Exit();
    }
  
    public void Enter(PlayerCon player)
    {
        if (!isActive)
        {
            enterPlayer = player;
            uiGroup.anchoredPosition = Vector3.zero;
            isActive = true;
        }
    }
    public void Exit()
    {
        if (isActive)
        {
            uiGroup.anchoredPosition = Vector3.down * 1000;
            isActive = false;
        }
    }
}
