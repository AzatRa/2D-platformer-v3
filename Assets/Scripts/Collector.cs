using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Health _health;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Apple>(out var apple))
        {
            _health.Heal(apple.Healing);
            _inventory.Take(apple);

            apple.Collect();
        }
    }
}
