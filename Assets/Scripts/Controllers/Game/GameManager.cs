using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameView _gameView;
    [SerializeField] private Player _player;
    [SerializeField] private InitInvents _invents;
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
        _gameView.OnAcceptPurchaseButtonTap += OnBuyItem;
    }
    private void OnDisable()
    {
        _player.OnTouchTradingCollider -= OnActivateAcceptTradingScreen;
        _gameView.OnAcceptTradingButtonTap -= OnActivateTradingScreen;
        _gameView.OnDenyTradingButtonTap -= OnDeactivateTradingScreen;
        _gameView.OnAcceptPurchaseButtonTap -= OnBuyItem;
    }
    private void OnActivateAcceptTradingScreen(bool value)
    {
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
    }
    public void SetCurrentItem(Item item)
    {
        _currentItem = item;
        _gameView.ActivateAcceptTradingScreen(true);
        _gameView.AcceptButtonState = AcceptTradingButtonState.Purchase;
        _gameView.ChangeAcceptButtonEvent();
        if(_currentItem.ItemInvent==_invents.ShopInvent)
        _gameView.SetTradingText($" Byuing item: {item.ItemName} Costs: {item.Price}$. Deal?",true);
        else if(_currentItem.ItemInvent == _invents.PlayerInvent)
            _gameView.SetTradingText($" Selling item: {item.ItemName} Costs: {item.Price}$. Deal?", true);
    }
    private void OnBuyItem()
    {
        if(_currentItem.ItemInvent==_invents.ShopInvent)
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
        else if(_currentItem.ItemInvent == _invents.PlayerInvent)
        {
            _coins += _currentItem.Price;
            _gameView.ChangeCoinsText(_coins.ToString());
            _invents.ShopInvent.AddItem(_currentItem);
            _currentItem.ItemInvent = _invents.ShopInvent;
            _invents.PlayerInvent.DeleteItem(_currentItem);
            _gameView.SetTradingText("Thanks!", false);
        }

    }
}
