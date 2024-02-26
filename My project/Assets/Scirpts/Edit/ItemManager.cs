using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public ItemList ItemList = null;
    public void OnRandomSetItemGrade(Item newItem)
    {
        if (newItem.ItemType == Item.ITEMTYPE.Equipment)
        {
            int Ran = Random.Range(1, 100);
            if (Ran <= 1)
            {
                newItem.ItemGrade = Item.EQUIPMENGRADE.Legendary;
            }
            else if (Ran > 1 && Ran <= 20)
            {
                newItem.ItemGrade = Item.EQUIPMENGRADE.Eqic;
            }
            else if (Ran > 20 && Ran <= 45)
            {
                newItem.ItemGrade = Item.EQUIPMENGRADE.Unique;
            }
            else if (Ran > 45 && Ran <= 70)
            {
                newItem.ItemGrade = Item.EQUIPMENGRADE.Rare;
            }
            else if (Ran > 70 && Ran <= 99)
            {
                newItem.ItemGrade = Item.EQUIPMENGRADE.Normal;
            }       
            newItem.StatPoint = (int)newItem.ItemGrade * 10;
        }
    }
    private void Awake()
    {
        base.Initialize();
    }

}
