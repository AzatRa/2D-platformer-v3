using UnityEngine;
using UnityEngine.Pool;

public class AppleSpawner : MonoBehaviour
{
    [SerializeField] private Apple _prefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private int _poolCapasity = 50;
    [SerializeField] private int _poolMaxSize = 100;

    private ObjectPool<Apple> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Apple>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (obj) => obj.gameObject.SetActive(true),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapasity,
            maxSize: _poolMaxSize);
    }

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        foreach (Transform spawnPoint in _spawnPoints)
        {
            Apple apple = Get();
            apple.transform.position = spawnPoint.position;
            apple.gameObject.SetActive(true);
            apple.OnCollected += OnCollected;
        }
    }

    private void OnCollected(Apple apple)
    {
        apple.OnCollected -= OnCollected;
        Release(apple);
    }

    private Apple Get()
    {
        return _pool.Get();
    }

    private void Release(Apple obj)
    {
        _pool.Release(obj);
    }
}