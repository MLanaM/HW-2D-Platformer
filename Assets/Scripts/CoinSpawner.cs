using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CoinSpawner : MonoBehaviour
{
    private const int MaxPoolSize = 5;
    private const float TimeBetweenCoins = 10f;

    [SerializeField] private Coin _coinPrefab;

    private IReadOnlyList<Vector3> _spawnPositions;
    private Queue<int> _availablePositionIndices;
    private ObjectPool<Coin> _pool;

    private Coroutine _coroutine;
    private WaitForSeconds _timeToCreateNewCoin = new(TimeBetweenCoins);

    public event Action<Coin> CoinActivatedEvent;
    public event Action<Coin> TurnOffCoinEvent;

    private void Awake()
    {
        FillSpawnPositions();

        _pool = new ObjectPool<Coin>(
            createFunc: CreateNewCoin,
            actionOnGet: ActivateCoin,
            actionOnRelease: coin => coin.gameObject.SetActive(false),
            actionOnDestroy: DestroyCoin,
            collectionCheck: true,
            defaultCapacity: MaxPoolSize,
            maxSize: MaxPoolSize);

        AddStartCoins();
    }

    private void OnEnable()
    {
        _coroutine = StartCoroutine(CreateCoinRoutine());
    }

    private void OnDisable()
    {
        if (_coroutine == null)
            return;

        StopCoroutine(_coroutine);
        _coroutine = null;
    }

    private void FillSpawnPositions()
    {
        _spawnPositions = new List<Vector3>
        {
            new Vector3(-9f, -1.5f, 0),
            new Vector3(-7.5f, 6.5f, 0f),
            new Vector3(4f, 4.5f, 0f),
            new Vector3(10.5f, 8.5f, 0f),
            new Vector3(18f, -4f, 0f)
        };

        _availablePositionIndices = new Queue<int>();

        for (int i = 0; i < _spawnPositions.Count; i++)
            _availablePositionIndices.Enqueue(i);
    }

    private void AddStartCoins()
    {
        int coinsToCreateCount = _availablePositionIndices.Count;

        for (int i = 0; i < coinsToCreateCount; i++)
            _pool.Get();
    }

    private IEnumerator CreateCoinRoutine()
    {
        while (enabled)
        {
            yield return new WaitUntil(() => _availablePositionIndices.Count > 0);
            yield return _timeToCreateNewCoin;

            _pool.Get();
        }
    }

    private Coin CreateNewCoin()
    {
        Coin newCoin = Instantiate(_coinPrefab);
        newCoin.OnCoinCollectedEvent += TurnOffCoin;

        return newCoin;
    }

    private void ActivateCoin(Coin coin)
    {
        int positionIndex = _availablePositionIndices.Dequeue();

        coin.transform.position = _spawnPositions[positionIndex];
        coin.SetSpawnPositionIndex(positionIndex);
        coin.gameObject.SetActive(true);

        CoinActivatedEvent?.Invoke(coin);
    }

    private void TurnOffCoin(Coin coin)
    {
        _pool.Release(coin);
        _availablePositionIndices.Enqueue(coin.SpawnPositionIndex);
        TurnOffCoinEvent?.Invoke(coin);
    }

    private void DestroyCoin(Coin coin)
    {
        coin.OnCoinCollectedEvent -= TurnOffCoin;
        Destroy(coin.gameObject);
    }
}
