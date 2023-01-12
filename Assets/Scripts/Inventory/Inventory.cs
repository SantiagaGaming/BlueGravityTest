using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
   [HideInInspector] public InventoryCell[] Cells { get; set; } = new InventoryCell[6];
    public int GetInventLenght => Cells.Length;
    public void AddItem(Item item)
    {
        InventoryCell tempCell = Cells.FirstOrDefault(i => i.Item == null);
        if (tempCell == null)
            return;
        tempCell.InitItem(item);
    }
    public void DeleteItem(Item item)
    {
        InventoryCell tempCell = Cells.FirstOrDefault(i => i.Item == item);
        if (tempCell == null)
            return;
        tempCell.Item = null;
    }
    public Item GetItemByName(string itemName)
    {
        foreach (var item in Cells)
        {
            if (item.Item.ItemName == itemName)
                return item.Item;
        }
         return null;
    }

}
