using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public UnityAction<bool> OnTouchTradingCollider;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
            coin.DeactivateCoin();
        if (collision.TryGetComponent(out TradingCollier tradingCollider))
        {
            OnTouchTradingCollider?.Invoke(true);
        }
           
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out TradingCollier tradingCollider))
            OnTouchTradingCollider?.Invoke(false);
    }
}
