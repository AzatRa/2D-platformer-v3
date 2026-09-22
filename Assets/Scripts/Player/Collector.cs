using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Health _health;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Strawberry>(out var strawberry))
        {
            _health.TakeValue(strawberry.Healing);
            _inventory.Take(strawberry);

            strawberry.Collect();
        }

        if (collision.TryGetComponent<Apple>(out var apple))
        {
            _inventory.Take(apple);

            apple.Collect();
        }
    }
}
