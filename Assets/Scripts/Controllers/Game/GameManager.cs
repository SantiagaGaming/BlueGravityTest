using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        if(Instance==null)
            Instance = this;
    }
    [SerializeField] private GameView _gameView;
 
    private int _coins;
    public void ChangeCoins(int coins)
    {
        _coins += coins;
        _gameView.ChangeCoinsText(_coins.ToString());
    }
}
