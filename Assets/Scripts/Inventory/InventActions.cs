using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum InventInteractionState
{
    None,
    Trade,
    Wear
}
public class InventActions : MonoBehaviour
{
    public UnityAction<AcceptTradingButtonState> OnTradingState;
    public UnityAction<string,bool> OnChangeTradingText;
    [SerializeField] private ItemOnPlayer _shirt;
    [SerializeField] private ItemOnPlayer _pants;
    [SerializeField] private InitInvents _invents;
    [SerializeField] private Player _player;

    private Item _currentItem;
    public InventInteractionState InventState { get; set; } = InventInteractionState.None;

    public void SetCurrentItem(Item item)
    {
        _currentItem = item;
        if (InventState == InventInteractionState.Trade)
        {
            OnTradingState?.Invoke(AcceptTradingButtonState.Purchase);
            if (_currentItem.ItemInvent == _invents.ShopInvent)
                OnChangeTradingText?.Invoke($" Buying item: {item.ItemName} Costs: {item.Price}$. Deal?",true);
            else if (_currentItem.ItemInvent == _invents.PlayerInvent)
                OnChangeTradingText?.Invoke($" Selling item: {item.ItemName} Costs: {item.Price}$. Deal?",true);
        }
        else if (InventState == InventInteractionState.Wear)
        {
            if (_currentItem.ItemInvent == _invents.PlayerInvent && _currentItem.Weared == false)
            {
                if (item.ItemType == ItemType.Pants)
                {
                    _pants.InitItem(item);
                    _currentItem.Weared = true;
                }

                else if (item.ItemType == ItemType.Shirt)
                {
                    _shirt.InitItem(item);
                    _currentItem.Weared = true;
                }
                _invents.PlayerInvent.DeleteItem(_currentItem);
            }
            else
                BuySellItem();
        }
    }
    public void BuySellItem()
    {
        if (InventState == InventInteractionState.Trade)
        {
            if (_currentItem.ItemInvent == _invents.ShopInvent)
            {
                if (_player.Coins >= _currentItem.Price)
                {
                    SoundPlayer.Instance.PlayTradeSound();
                   _player.ChangeCoins(-_currentItem.Price);
                    _invents.PlayerInvent.AddItem(_currentItem);
                    _currentItem.ItemInvent = _invents.PlayerInvent;
                    _invents.ShopInvent.DeleteItem(_currentItem);
                    OnChangeTradingText?.Invoke("Thanks!", false);
                }
                else
                {
                    OnChangeTradingText?.Invoke($"Sorry, You don't have money for Item {_currentItem.ItemName}.", false);
                }
            }
            else if (_currentItem.ItemInvent == _invents.PlayerInvent)
            {
                SoundPlayer.Instance.PlayTradeSound();
                _player.ChangeCoins(_currentItem.Price);
                _invents.ShopInvent.AddItem(_currentItem);
                _currentItem.ItemInvent = _invents.ShopInvent;
                _invents.PlayerInvent.DeleteItem(_currentItem);
                OnChangeTradingText?.Invoke("Thanks!", false);
            }
        }
        else if (_currentItem.Weared == true)
        {
            _currentItem.Weared = false;
            _invents.PlayerInvent.AddItem(_currentItem);
            if (_currentItem.ItemType == ItemType.Pants)
                _pants.InitItem(null);
            else if (_currentItem.ItemType == ItemType.Shirt)
                _shirt.InitItem(null);
        }
    }
}
