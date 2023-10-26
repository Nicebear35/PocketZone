using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private RectTransform _inventoryButtonPosition;
    [SerializeField] private float _timeToMove;

    private bool _isOpened;
    private Vector3 _targetPosition;
    private Vector3 _targetScale;
    private Coroutine _openInventoryCoroutine;

    private void Start()
    {
        _isOpened = false;
        _targetPosition = _isOpened ? _inventoryButtonPosition.localPosition : Vector3.zero;
        _targetScale = _isOpened ? Vector3.zero : Vector3.one;
        transform.position = _inventoryButtonPosition.transform.position;
        transform.localScale = Vector3.zero;
    }

    private IEnumerator ShowOrHideInventory(Vector3 targetPosition, Vector3 targetScale)
    {
        while (transform.localPosition != targetPosition)
        {
            Debug.Log($"current position - {transform.localPosition} / target - {targetPosition}");

            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, _timeToMove);
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, _timeToMove);
            yield return null;

            Debug.Log($"position - {transform.localPosition} / scale - {transform.localScale}");
        }

        _isOpened = !_isOpened;
    }

    public void OpenOrCloseInventory()
    {
        if (_openInventoryCoroutine == null)
        {
            _openInventoryCoroutine = StartCoroutine(ShowOrHideInventory(_targetPosition, _targetScale));
        }
        else
        {
            StopCoroutine(_openInventoryCoroutine);
        }
    }
}
