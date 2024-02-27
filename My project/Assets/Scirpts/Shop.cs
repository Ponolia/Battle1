using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public RectTransform uiGroup;
    PlayerCon enterPlayer;

    bool isActive;
    public void Enter(PlayerCon player)
    {
        if (!isActive)
        {
            enterPlayer = player;
            uiGroup.anchoredPosition = Vector3.zero;
            isActive = true;
        }
    }
}
