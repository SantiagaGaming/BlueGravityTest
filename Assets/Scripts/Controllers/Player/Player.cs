using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public UnityAction<bool> OnTouchTradingCollider;
    public UnityAction <int> OnCoinsChanged;

    private int _coins;
    public int Coins => _coins;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
            coin.DeactivateCoin();
        if (collision.TryGetComponent(out TradingCollider tradingCollider))
        {
            OnTouchTradingCollider?.Invoke(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out TradingCollider tradingCollider))
            OnTouchTradingCollider?.Invoke(false);
    }
    public void ChangeCoins(int coins)
    {
        _coins += coins;
       OnCoinsChanged?.Invoke(_coins);
    }
}
