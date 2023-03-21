using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class MenuUI : MonoBehaviour
{
    public static Action GoodSound;
    public static Action NotEnoughMoney;
    [SerializeField] private ItemToShop[] _items;
    [SerializeField] private GameObject[] _pers;
    [SerializeField] private TMP_Text _buttonText, _curMoney;
    private int _selectedIndex = 0;
    private void Start()
    {
        ShowItem();
    }
    public void UpItem()
    {
        _selectedIndex++;
        if (_selectedIndex >= _items.Length)
        {
            _selectedIndex = 0;
        }
        ShowItem();
    }
    public void DownItem()
    {
        _selectedIndex--;
        if (_selectedIndex < 0)
        {
            _selectedIndex = _items.Length-1;
        }
        ShowItem();
    }
    public void BuyItem()
    {
        if (!Player.Instance.data.indexBought.Contains(_selectedIndex))
        {
            if (Player.Instance.data.money >= _items[_selectedIndex].price)
            {
                Player.Instance.data.money -= _items[_selectedIndex].price;
                Player.Instance.data.indexBought.Add(_selectedIndex);
                Player.Instance.data.indexPicked = _selectedIndex;
                UpdateUI();
                GoodSound?.Invoke();
            }
            else
            {
                NotEnoughMoney?.Invoke();
            }
        }
        else
        {
            Player.Instance.data.indexPicked = _selectedIndex;
            UpdateUI();
        }
        Player.Instance.Save();
    }
    private void ShowItem()
    {
        foreach (var item in _pers)
        {
            if (item.name == _items[_selectedIndex].name)
            {
                item.SetActive(true);
            }
            else item.SetActive(false);
        }
        UpdateUI();
    }
    private void UpdateUI()
    {
        _curMoney.text = Player.Instance.data.money.ToString();
        if (Player.Instance.data.indexPicked == _selectedIndex)
        {
            _buttonText.text = "Выбрано";
        }
        else if (Player.Instance.data.indexBought.Contains(_selectedIndex))
        {
            _buttonText.text = "Куплено";
        }
        else
        {
            _buttonText.text = _items[_selectedIndex].price.ToString();
        }
    }
}
