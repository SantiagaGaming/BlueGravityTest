using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitInvents : MonoBehaviour
{
    [SerializeField] private Inventory _playerInvent;
    [SerializeField] private Inventory _shopInvent;
    [SerializeField] private InventoryCell _prefub;
    [SerializeField] private Item[] _items;
    public Inventory PlayerInvent => _playerInvent;
    public Inventory ShopInvent => _shopInvent;

    private float _yPos = 2f;
    private float _xPos;
    private float _step = 100;

    private void Start()
    {
        FillInvent(_shopInvent);
        FillInvent(_playerInvent);
        AddRandomItemsToInvent(_shopInvent,3);
    }
    private void FillInvent(Inventory invent)
    {
        _xPos = -230;
        for (int i = 0; i < invent.GetInventLenght-1; i++)
        {
            InventoryCell temp = Instantiate(_prefub, invent.transform);
            invent.Cells[i] = temp;
            temp.transform.position += new Vector3(_xPos, _yPos);
            _xPos += _step;
        }
    }
    private void AddRandomItemsToInvent(Inventory invent, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Item temp = Instantiate(_items[Random.Range(0, _items.Length)]);
            if (temp != null)
            {
                invent.AddItem(temp);
                temp.ItemInvent = invent;
            }
               
        }
    }
}
