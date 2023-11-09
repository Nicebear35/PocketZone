using UnityEngine;
using UnityEngine.Events;

public class LootingDetector : MonoBehaviour
{
    public event UnityAction<DropingItem> OnDropItemFound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out DropingItem item))
        {
            OnDropItemFound?.Invoke(item);
        }
    }
}
