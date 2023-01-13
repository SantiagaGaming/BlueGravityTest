using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public void DeactivateCoin()
    {
        Player player  =FindObjectOfType<Player>();
        player.ChangeCoins(1);
        SoundPlayer.Instance.PlayCoinSound();
        gameObject.SetActive(false);
    }    
}
