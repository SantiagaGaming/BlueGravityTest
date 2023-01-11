using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private MenuView _menuView;
    [SerializeField] private SceneChanger _sceneChanger;
    private void OnEnable()
    {
        _menuView.OnStartButtonClick += OnLoadLevel;
        _menuView.OnExitButtonClick += OnExitGame;
    }
    private void OnDisable()
    {
        _menuView.OnStartButtonClick -= OnLoadLevel;
        _menuView.OnExitButtonClick -= OnExitGame;
    }
    private void OnLoadLevel()
    {
       _sceneChanger.ChangeScene("Game");
    }
    private void OnExitGame()
    {
        Application.Quit();
    }
}
