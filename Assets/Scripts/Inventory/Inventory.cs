using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private RectTransform _inventoryButtonTransform;
    [SerializeField] private float _timeToScaleUp;
    [SerializeField] private float _timeToScaleDown;
    [SerializeField] private float _timeToMove;
    [SerializeField] private List<InventoryCell> _cells;

    private Scrollbar _scrollBar;
    private float _topScrollPositon;
    private float _timeToScale;
    private bool _isOpened;
    private bool _isFull;
    private bool _noFreeCells;
    private Vector3 _targetPosition;
    private Vector3 _targetScale;
    private Coroutine _openInventoryCoroutine;


    private void Start()
    {
        _topScrollPositon = 1f;
        _isOpened = false;
        transform.localPosition = _inventoryButtonTransform.localPosition;
        transform.localScale = Vector3.zero;
        _scrollBar = GetComponentInChildren<Scrollbar>();
    }

    private IEnumerator ShowOrHideInventory(Vector3 targetPosition, Vector3 targetScale)
    {
        while (transform.localPosition != targetPosition)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, _timeToMove);
            yield return null;
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, _timeToScale);
            yield return null;
        }

        _scrollBar.value = _topScrollPositon;
        _isOpened = !_isOpened;
    }

    private void AddItem(InventoryCell cell, InventoryItem item)
    {
        if (cell.IsEmpty)
        {
            Instantiate(item, cell.transform);
        }
    }

    public bool TryToAddItem(InventoryItem item)
    {
        foreach (var cell in _cells)
        {
            if (cell.TryToAddItem(item))
            {
                AddItem(cell, item);
                return true;
            }
        }

        if (_isFull || _noFreeCells)
        {
            return false;
        }

        return true;
    }

    public void DeletePickedItem(InventoryItem item)
    {
        Destroy(item);
    }

    public void OpenOrCloseInventory()
    {
        _timeToScale = _isOpened ? _timeToScaleDown : _timeToScaleUp;
        _targetPosition = _isOpened ? _inventoryButtonTransform.localPosition : Vector3.zero;
        _targetScale = _isOpened ? Vector3.zero : Vector3.one;

        if (_openInventoryCoroutine != null)
        {
            StopCoroutine(_openInventoryCoroutine);
        }

        _openInventoryCoroutine = StartCoroutine(ShowOrHideInventory(_targetPosition, _targetScale));
    }
}
