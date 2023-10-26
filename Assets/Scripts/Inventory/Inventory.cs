using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Button _inventoryButton;

    private bool _isOpened;

    private void Start()
    {
        _isOpened = false;
        transform.position = _inventoryButton.transform.position;
        transform.localScale = Vector3.zero;
    }

    public IEnumerator ShowOrHideInventory(Vector3 position, Vector3 scale)
    {
        while (transform.localScale != position && transform.position != scale)
        {
            // тут будет корутина сворачивания и разворачивания инвентаря 
            // он будет прятаться за кнопку и уменьшаться до нуля
            yield return null;
        }

    }
}
