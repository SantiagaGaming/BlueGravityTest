using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuView : MonoBehaviour
{
    public UnityAction OnStartButtonClick;
    public UnityAction OnExitButtonClick;

    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _exitGameButton;
    private void Start()
    {
        _startGameButton.onClick.AddListener(OnStartButtonClick);
        _exitGameButton.onClick.AddListener(OnExitButtonClick);
    }
}
