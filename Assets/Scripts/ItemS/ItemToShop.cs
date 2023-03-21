using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Item",menuName = "Items", order = 51)]
public class ItemToShop : ScriptableObject
{
    public int price;
    public string name;
}
