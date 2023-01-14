using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public enum AcceptTradingButtonState
{
    EnterShop,
    Purchase
}
public class GameView : MonoBehaviour
{
    public UnityAction OnAcceptTradingButtonTap;
    public UnityAction OnAcceptPurchaseButtonTap;
    public UnityAction OnDenyTradingButtonTap;
    public UnityAction OnInventButtonTap;

    [HideInInspector] public AcceptTradingButtonState AcceptButtonState = AcceptTradingButtonState.EnterShop;

    [SerializeField] private Text _coinsText;
    [SerializeField] private Text _tradingText;

    [SerializeField] private GameObject _acceptTradingScreen;
    [SerializeField] private GameObject _acceptTradingButton;
    [SerializeField] private GameObject _denyTradingButton;
    [SerializeField] private GameObject _playerInventory;
    [SerializeField] private GameObject _shopInventory;
    [SerializeField] private GameObject _inventButton;
    [SerializeField] private GameObject _pantsInventCell;
    [SerializeField] private GameObject _shirtInventCell;

    private bool _tellStory = true;
    private void Start()
    {
        _acceptTradingButton.GetComponent<Button>().onClick.AddListener(OnAcceptTradingButtonTap);
        _denyTradingButton.GetComponent<Button>().onClick.AddListener(OnDenyTradingButtonTap);
        _inventButton.GetComponent<Button>().onClick.AddListener(OnInventButtonTap);
    }
    public void ChangeCoinsText(string coins)
    {
        _coinsText.text = "Coins: " + coins;
    }
    public void ActivateAcceptTradingScreen(bool value)
    {
        _acceptTradingScreen.SetActive(value);
        if (!value)
        {
            _acceptTradingButton.SetActive(false);
            _denyTradingButton.SetActive(false);
            _tellStory = false;
        }
        else _tellStory = true;
    }
    public void SetTradingText(string text, bool answer)
    {
        StartCoroutine(BreakAndRunStoryText(text,answer));
    }

    public void ActivatePlayerInventory(bool value)
    {
        _pantsInventCell.SetActive(false);
        _shirtInventCell.SetActive(false);
        _playerInventory.SetActive(value);
    }
    public void ActivatePlayerInventoryWithItems(bool value)
    {
        _pantsInventCell.SetActive(value);
        _shirtInventCell.SetActive(value);
        _playerInventory.SetActive(value);
    }
    public void ActivateShopInventory(bool value)
    {
        _shopInventory.SetActive(value);
    }
    private IEnumerator StoryStyleTextCo(string text, bool answer)
    {
        foreach (var item in text)
        {
            if (!_tellStory)
                 break;
            SoundPlayer.Instance.PlayTextSound();
            _tradingText.text += item;
            yield return new WaitForSeconds(0.09f);
        }
        if(answer && _tellStory)
        {
            _acceptTradingButton.SetActive(true);
            _denyTradingButton.SetActive(true);
        }
    }
    private IEnumerator BreakAndRunStoryText(string text, bool answer)
    {
        _acceptTradingButton.SetActive(false);
        _denyTradingButton.SetActive(false);
        _tradingText.text = "";
        _tellStory = false;
        yield return new WaitForSeconds(0.1f);
        _tellStory = true;
        StartCoroutine(StoryStyleTextCo(text, answer));
    }
    public void ChangeAcceptButtonEvent()
    {
        _acceptTradingButton.GetComponent<Button>().onClick.RemoveAllListeners();
        if (AcceptButtonState == AcceptTradingButtonState.EnterShop)
        {
            _acceptTradingButton.GetComponent<Button>().onClick.AddListener(OnAcceptTradingButtonTap);
        }
        else if (AcceptButtonState == AcceptTradingButtonState.Purchase)
            _acceptTradingButton.GetComponent<Button>().onClick.AddListener(OnAcceptPurchaseButtonTap);

    }
    public bool InventStatus()
    {
        if (_playerInventory.activeSelf == true)
            return true;
        else return false;
    }
    public void EnableInventButton(bool value)
    {
        _inventButton.SetActive(value);
    }
}
