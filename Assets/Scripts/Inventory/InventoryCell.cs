using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCell : MonoBehaviour
{
   [HideInInspector] public Item Item { get; set; }
    public virtual void InitItem(Item item)
    {
        Item = item;
        if (item == null)
            return;
        Item.transform.position = transform.position;
        item.transform.SetParent(transform);
    }
}
