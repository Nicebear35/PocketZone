using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class LootingSystem : MonoBehaviour
{
    [SerializeField] private List<DropingItem> _itemsToDrop;
    [SerializeField] private List<InventoryItem> _inventoryItems;
    [SerializeField] private Player _player;
    [SerializeField] private LootingDetector _lootingDetector;

    private List<KeyValuePair<DropingItem, InventoryItem>> _items;

    public event UnityAction<InventoryItem> OnInventoryItemFound;

    private void Start()
    {
        _items = new List<KeyValuePair<DropingItem, InventoryItem>>();

        for (int i = 0; i < _itemsToDrop.Count; i++)
        {
            _items.Add(new KeyValuePair<DropingItem, InventoryItem>(_itemsToDrop[i], _inventoryItems[i]));
        }
    }

    private void OnEnable()
    {
        _lootingDetector.OnDropItemFound += PutItemToInventory;
    }

    private void OnDisable()
    {
        _lootingDetector.OnDropItemFound -= PutItemToInventory;
    }

    public DropingItem ChooseRandomItem()
    {
        DropingItem itemItemToDrop = null;

        int chanceSum = 0;

        for (int i = 0; i < _itemsToDrop.Count; i++)
        {
            chanceSum += _itemsToDrop[i].DropChance;
        }

        int resultChance = Random.Range(0, chanceSum);

        for (int i = 0; i < _itemsToDrop.Count; i++)
        {
            resultChance -= _itemsToDrop[i].DropChance;

            if (resultChance < 0)
            {
                itemItemToDrop = _itemsToDrop[i];
                break;
            }
        }

        return itemItemToDrop;
    }

    public void PutItemToInventory(DropingItem dropItem)
    {
        InventoryItem itemToReturn = null;

        foreach (var item in _items)
        {
            if (item.Key.Name == dropItem.Name)
            {
                itemToReturn = item.Value;
            }
        }

        OnInventoryItemFound?.Invoke(itemToReturn);
    }
}
