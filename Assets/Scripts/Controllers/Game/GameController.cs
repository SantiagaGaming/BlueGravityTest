using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameView _gameView;
    [SerializeField] private Player _player;
    [SerializeField] private InventActions _inventActions;
    private void OnEnable()
    {
        _player.OnCoinsChanged += OnChangeCoins;
        _player.OnTouchTradingCollider += OnActivateAcceptTradingScreen;
        _gameView.OnAcceptTradingButtonTap += OnActivateTradingScreen;
        _gameView.OnDenyTradingButtonTap += OnDeactivateTradingScreen;
        _gameView.OnAcceptPurchaseButtonTap += OnBuySellItem;
        _gameView.OnInventButtonTap += OnShowPlayerInvent;
        _inventActions.OnTradingState += OnPurchaseAction;
        _inventActions.OnChangeTradingText += OnChangeTradingText;
    }
    private void OnDisable()
    {
        _player.OnCoinsChanged -= OnChangeCoins;
        _player.OnTouchTradingCollider -= OnActivateAcceptTradingScreen;
        _gameView.OnAcceptTradingButtonTap -= OnActivateTradingScreen;
        _gameView.OnDenyTradingButtonTap -= OnDeactivateTradingScreen;
        _gameView.OnAcceptPurchaseButtonTap -= OnBuySellItem;
        _gameView.OnInventButtonTap -= OnShowPlayerInvent;
        _inventActions.OnTradingState -= OnPurchaseAction;
        _inventActions.OnChangeTradingText -= OnChangeTradingText;
    }

    public void OnChangeCoins(int coins)
    {
        _gameView.ChangeCoinsText(coins.ToString());
    }
    private void OnChangeTradingText(string text,bool value)
    {
        _gameView.SetTradingText(text,value);
    }
    private void OnActivateAcceptTradingScreen(bool value)
    {
        if (_inventActions.InventState == InventInteractionState.Wear)
            return;
        _gameView.EnableInventButton(!value);
        _inventActions.InventState = InventInteractionState.Trade;
        _gameView.AcceptButtonState = AcceptTradingButtonState.EnterShop;
        _gameView.ChangeAcceptButtonEvent();
        _gameView.ActivateAcceptTradingScreen(value);
        if (value)
            _gameView.SetTradingText("Do You wanna trade?", true);
        else
        {
            _gameView.ActivateShopInventory(false);
            _gameView.ActivatePlayerInventory(false);
        }
    }
    private void OnPurchaseAction(AcceptTradingButtonState state)
    {
        _gameView.ActivateAcceptTradingScreen(true);
        _gameView.AcceptButtonState = state;
        _gameView.ChangeAcceptButtonEvent();
    }

    private void OnActivateTradingScreen()
    {
        _gameView.ActivateShopInventory(true);
        _gameView.ActivatePlayerInventory(true);
    }
    private void OnDeactivateTradingScreen()
    {
        OnActivateAcceptTradingScreen(false);
        _inventActions.InventState = InventInteractionState.None;
    }
    private void OnShowPlayerInvent()
    {
        if (!_gameView.InventStatus())
        {
            _gameView.ActivatePlayerInventoryWithItems(true);
            _inventActions.InventState = InventInteractionState.Wear;
        }
        else
        {
            _gameView.ActivatePlayerInventoryWithItems(false);
            _inventActions.InventState = InventInteractionState.None;
        }
    }
    private void OnBuySellItem()
    {
        _inventActions.BuySellItem();
    }
}
