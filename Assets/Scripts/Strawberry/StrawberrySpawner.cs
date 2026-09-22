using UnityEngine;
using UnityEngine.Pool;

public class StrawberrySpawner : MonoBehaviour
{
    [SerializeField] private Strawberry _prefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private int _poolCapasity = 50;
    [SerializeField] private int _poolMaxSize = 100;

    private ObjectPool<Strawberry> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Strawberry>(
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
            Strawberry strawberry = Get();
            strawberry.transform.position = spawnPoint.position;
            strawberry.gameObject.SetActive(true);
            strawberry.Collected += Collected;
        }
    }

    private void Collected(Strawberry strawberry)
    {
        strawberry.Collected -= Collected;
        Release(strawberry);
    }

    private Strawberry Get()
    {
        return _pool.Get();
    }

    private void Release(Strawberry obj)
    {
        _pool.Release(obj);
    }
}
