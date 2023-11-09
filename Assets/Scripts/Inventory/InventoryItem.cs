using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IInventoryItem
{
    [SerializeField] private Image _image;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private GameObject _deleteItemWindow;
    [SerializeField] private int _dropChance;
    [SerializeField] private int _maxAmount;
    [SerializeField] private int _currentAmount;
    [SerializeField] private TextMeshProUGUI _amountText;

    public Image Image => _image;
    public string Name => _name;
    public string Description => _description;
    public GameObject DeleteItemWindow => _deleteItemWindow;
    public int DropChance => _dropChance;
    public int MaxAmount => _maxAmount;
    public int CurrentAmount => _currentAmount;

    public UnityAction<InventoryItem> OnItemClicked;

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        EnableDeleteWindow();
        OnItemClicked?.Invoke(this);
    }

    public bool TryAddSameItem()
    {
        if (_currentAmount < _maxAmount)
        {
            _currentAmount++;
            _amountText.text = _currentAmount > 1 ? _currentAmount.ToString() : " ";
            return true;
        }

        return false;
    }

    private void EnableDeleteWindow()
    {
        _deleteItemWindow.gameObject.SetActive(true);
        _deleteItemWindow.transform.parent = GetComponentInParent<Transform>();
        _deleteItemWindow.transform.localPosition = Vector3.zero;
    }

    public void Destroy()
    {
        Destroy(gameObject);
        _deleteItemWindow.gameObject.SetActive(false);
    }
}
