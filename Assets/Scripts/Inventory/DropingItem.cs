using UnityEngine;

public class DropingItem : MonoBehaviour
{
    [SerializeField] private int _chanceToDrop;
    [SerializeField] string _name;

    public int DropChance => _chanceToDrop;
    public string Name => _name;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out LootingDetector detector))
        {
            Destroy(gameObject);
        }
    }
}
