using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameView _gameView;
    [SerializeField] private Player _player;
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

    }
    private void OnDisable()
    {
        _player.OnTouchTradingCollider -= OnActivateAcceptTradingScreen;
        _gameView.OnAcceptTradingButtonTap -= OnActivateTradingScreen;
        _gameView.OnDenyTradingButtonTap -= OnDeactivateTradingScreen;
    }
    private void OnActivateAcceptTradingScreen(bool value)
    {
        _gameView.ActivateAcceptTradingScreen(value);
        if (value)
        _gameView.SetTradingText("Do You wanna trade?");
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
}
