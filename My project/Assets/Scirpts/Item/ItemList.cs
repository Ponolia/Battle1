using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemList", menuName = "ItemObject/ItemList", order = int.MaxValue)]

public class ItemList : ScriptableObject
{
    [SerializeField]
    public List<Item> Items = new List<Item>();
}
