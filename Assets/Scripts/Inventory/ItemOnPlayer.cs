using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemOnPlayer : InventoryCell
{

    [SerializeField] private ItemType _currentItemType;
    [SerializeField] private PlayerClothes _clothes;
    public override void InitItem(Item item)
    {
        base.InitItem(item);
        if (item != null)
            _clothes.SetCloth(item.ItemColor);
        else _clothes.SetCloth();
    }
}
