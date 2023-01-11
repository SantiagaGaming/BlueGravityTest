using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField] private Text _coinsText;
    public void ChangeCoinsText(string coins)
    {
        _coinsText.text = "Coins: " + coins;
    }
}
