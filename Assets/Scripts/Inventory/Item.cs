using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    Pants,
    Shirt
}
public enum ItemColor
{
    Red,
    Yellow
}
public class Item : MonoBehaviour
{
   [SerializeField] private string _itemName;
   [SerializeField] private int _price;
   [SerializeField] private ItemType _itemType;
   [SerializeField] private ItemColor _itemColor;
    public string ItemName => _itemName;
    public int Price => _price;
    public ItemType ItemType => _itemType;
    public ItemColor ItemColor => _itemColor;
}
