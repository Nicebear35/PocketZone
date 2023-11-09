using UnityEngine;

public class InventoryCell : MonoBehaviour
{
    private bool _isOccupied;
    private bool _isFull;
    private bool _isEmpty;
    private int _itemsInsideCount;
    private InventoryItem _item;

    public bool IsOccupied => _isOccupied;
    public bool IsFull => _isFull;
    public bool IsEmpty => _isEmpty;
    public int ItemsInsideCount => _itemsInsideCount;
    public InventoryItem Item => _item;

    private void Start()
    {
        _isFull = false;
        _isOccupied = false;
    }

    public bool TryToAddItem(InventoryItem item)
    {
        _isOccupied = GetComponentInChildren<InventoryItem>() != null;
        _isEmpty = GetComponentInChildren<InventoryItem>() == null;

        if (_isEmpty)
        {
            return true;
        }

        if (_isOccupied)
        {
            _item = GetComponentInChildren<InventoryItem>();
            _isFull = _item.CurrentAmount == _item.MaxAmount;
         
            if (!_isFull && _item.Name == item.Name)
            {
                _item.TryAddSameItem();
                _itemsInsideCount = _item.CurrentAmount;
                return true;
            }
        }

        return false;
    }

}
