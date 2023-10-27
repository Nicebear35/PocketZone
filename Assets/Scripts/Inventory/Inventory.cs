using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private RectTransform _inventoryButtonTransform;
    [SerializeField] private float _timeToScaleUp;
    [SerializeField] private float _timeToScaleDown;
    [SerializeField]private float _timeToMove;

    private float _timeToScale;
    private bool _isOpened;
    private Vector3 _targetPosition;
    private Vector3 _targetScale;
    private Coroutine _openInventoryCoroutine;

    private void Start()
    {
        _isOpened = false;
        transform.localPosition = _inventoryButtonTransform.localPosition; //2
        transform.localScale = Vector3.zero;
    }

    private IEnumerator ShowOrHideInventory(Vector3 targetPosition, Vector3 targetScale)
    {
        while (transform.localPosition != targetPosition)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, _timeToMove); //2
            yield return null;
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, _timeToScale);
            yield return null;
        }

        _isOpened = !_isOpened;
        Debug.Log(_isOpened);
    }

    public void OpenOrCloseInventory()
    {
        _timeToScale = _isOpened ? _timeToScaleDown : _timeToScaleUp;
        _targetPosition = _isOpened ? _inventoryButtonTransform.localPosition : Vector3.zero; //1
        _targetScale = _isOpened ? Vector3.zero : Vector3.one;

        if (_openInventoryCoroutine != null)
        {
            StopCoroutine(_openInventoryCoroutine);
        }

        _openInventoryCoroutine = StartCoroutine(ShowOrHideInventory(_targetPosition, _targetScale));

    }
}
