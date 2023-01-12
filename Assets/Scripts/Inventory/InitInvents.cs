using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitInvents : MonoBehaviour
{
    [SerializeField] private Inventory _playerInvent;
    [SerializeField] private Inventory _shopInvent;
    [SerializeField] private InventoryCell _prefub;

    private float _yPos = 2f;
    private float _xPos;
    private float _step = 39;

    private void Start()
    {
        FillInvent(_shopInvent);
        FillInvent(_playerInvent);
    }
    private void FillInvent(Inventory invent)
    {
        _xPos = -111;
        for (int i = 0; i < invent.GetInventLenght-1; i++)
        {
            InventoryCell temp = Instantiate(_prefub, invent.transform);
            invent.Cells[i] = temp;
            temp.transform.position += new Vector3(_xPos, _yPos);
            _xPos += _step;
        }
    }
}
