using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public void DeactivateCoin()
    {
        GameManager.Instance.ChangeCoins(1);
        gameObject.SetActive(false);
    }    
}
