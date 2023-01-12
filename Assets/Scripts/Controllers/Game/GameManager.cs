using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class GameManager : MonoBehaviour
{
    public enum InventInteractionState
    {
        None,
        Trade,
        Wear
    }
    public static GameManager Instance;

    [SerializeField] private GameView _gameView;
    [SerializeField] private Player _player;
    [SerializeField] private InitInvents _invents;
    [SerializeField] private ItemOnPlayer _shirt;
    [SerializeField] private ItemOnPlayer _pants;

    public InventInteractionState InventState { get; private set; } = InventInteractionState.None;
    private Item _currentItem;
    private int _coins;
    private void Awake()
    {
        if(Instance==null)
            Instance = this;
    }
    public void ChangeCoins(int coins)
    {
        _coins += coins;
        _gameView.ChangeCoinsText(_coins.ToString());
    }
    private void OnEnable()
    {
        _player.OnTouchTradingCollider += OnActivateAcceptTradingScreen;
        _gameView.OnAcceptTradingButtonTap += OnActivateTradingScreen;
        _gameView.OnDenyTradingButtonTap += OnDeactivateTradingScreen;
        _gameView.OnAcceptPurchaseButtonTap += OnBuySellItem;
        _gameView.OnInventButtonTap += OnShowPlayerInvent;
    }
    private void OnDisable()
    {
        _player.OnTouchTradingCollider -= OnActivateAcceptTradingScreen;
        _gameView.OnAcceptTradingButtonTap -= OnActivateTradingScreen;
        _gameView.OnDenyTradingButtonTap -= OnDeactivateTradingScreen;
        _gameView.OnAcceptPurchaseButtonTap -= OnBuySellItem;
        _gameView.OnInventButtonTap -= OnShowPlayerInvent;
    }
    private void OnActivateAcceptTradingScreen(bool value)
    {
        if (InventState == InventInteractionState.Wear)
            return;
        _gameView.EnableInventButton(!value);
        InventState = InventInteractionState.Trade;
        _gameView.AcceptButtonState = AcceptTradingButtonState.EnterShop;
        _gameView.ChangeAcceptButtonEvent();
        _gameView.ActivateAcceptTradingScreen(value);
        if (value)
        _gameView.SetTradingText("Do You wanna trade?",true);
        else
        {
            _gameView.ActivateShopInventory(false);
            _gameView.ActivatePlayerInventory(false);
        }
    }
    private void OnActivateTradingScreen()
    {
        _gameView.ActivateShopInventory(true);
        _gameView.ActivatePlayerInventory(true);
    }
    private void OnDeactivateTradingScreen()
    {
        OnActivateAcceptTradingScreen(false);
        InventState = InventInteractionState.None;
    }
    public void SetCurrentItem(Item item)
    {
        _currentItem = item;
        if (InventState ==InventInteractionState.Trade)
        {
            _gameView.ActivateAcceptTradingScreen(true);
            _gameView.AcceptButtonState = AcceptTradingButtonState.Purchase;
            _gameView.ChangeAcceptButtonEvent();
            if (_currentItem.ItemInvent == _invents.ShopInvent)
                _gameView.SetTradingText($" Buying item: {item.ItemName} Costs: {item.Price}$. Deal?", true);
            else if (_currentItem.ItemInvent == _invents.PlayerInvent)
                _gameView.SetTradingText($" Selling item: {item.ItemName} Costs: {item.Price}$. Deal?", true);
        }
        else if(InventState == InventInteractionState.Wear)
        {
            if(_currentItem.ItemInvent == _invents.PlayerInvent && _currentItem.Weared == false)
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
                OnBuySellItem();

        }
    }
    private void OnBuySellItem()
    {
        if (InventState == InventInteractionState.Trade)
        {
            if (_currentItem.ItemInvent == _invents.ShopInvent)
            {
                if (_coins >= _currentItem.Price)
                {
                    _coins -= _currentItem.Price;
                    _gameView.ChangeCoinsText(_coins.ToString());
                    _invents.PlayerInvent.AddItem(_currentItem);
                    _currentItem.ItemInvent = _invents.PlayerInvent;
                    _invents.ShopInvent.DeleteItem(_currentItem);
                    _gameView.SetTradingText("Thanks!", false);
                }
                else
                {
                    _gameView.SetTradingText($"Sorry, You don't have money for Item {_currentItem.ItemName}.", false);
                }
            }
            else if (_currentItem.ItemInvent == _invents.PlayerInvent)
            {
                _coins += _currentItem.Price;
                _gameView.ChangeCoinsText(_coins.ToString());
                _invents.ShopInvent.AddItem(_currentItem);
                _currentItem.ItemInvent = _invents.ShopInvent;
                _invents.PlayerInvent.DeleteItem(_currentItem);
                _gameView.SetTradingText("Thanks!", false);
            }
        }
        else if( _currentItem.Weared == true)
        {
            _currentItem.Weared = false;
            _invents.PlayerInvent.AddItem(_currentItem);
            if (_currentItem.ItemType == ItemType.Pants)
                _pants.InitItem(null);
            else if(_currentItem.ItemType == ItemType.Shirt)
                _shirt.InitItem(null);
        }

    }
    private void OnShowPlayerInvent()
    {
        if(!_gameView.InventStatus())
        {
            _gameView.ActivatePlayerInventoryWithItems(true);
            InventState = InventInteractionState.Wear;
        }
        else
        {
            _gameView.ActivatePlayerInventoryWithItems(false);
            InventState = InventInteractionState.None;

        }

    }
}
