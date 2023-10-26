using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private bool _isOpened;
    private Button _inventoryButton;

    public IEnumerator ShowInventory()
    {
        yield return null;
    }
}
