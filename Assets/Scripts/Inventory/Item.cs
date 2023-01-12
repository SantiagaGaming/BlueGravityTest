using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum ItemType
{
    Pants,
    Shirt
}
public enum ItemColor
{
    Green,
    Yellow
}
public class Item : MonoBehaviour
{
   [SerializeField] private string _itemName;
   [SerializeField] private int _price;
   [SerializeField] private ItemType _itemType;
   [SerializeField] private ItemColor _itemColor;
    [HideInInspector] public Inventory ItemInvent;
    private Button _button;
    private void Start()
    {
        _button= GetComponent<Button>();
        _button.onClick.AddListener(SetCurrentItem);
    }
    public string ItemName => _itemName;
    public int Price => _price;
    public ItemType ItemType => _itemType;
    public ItemColor ItemColor => _itemColor;
    private void SetCurrentItem()
    {
        GameManager.Instance.SetCurrentItem(this);
    }
}
