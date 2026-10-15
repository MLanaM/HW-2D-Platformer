using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private CoinSpawner _coinSpawner;

    private int _coinCount;
    private List<Coin> _subscribedCoins;

    private void Awake()
    {
        _coinCount = 0;
        _subscribedCoins = new List<Coin>();
    }

    private void OnEnable()
    {
        _coinSpawner.CoinActivatedEvent += SubscribeCoin;
        _coinSpawner.TurnOffCoinEvent += UnsubscribeCoin;
    }

    private void OnDisable()
    {
        _coinSpawner.CoinActivatedEvent -= SubscribeCoin;
        _coinSpawner.TurnOffCoinEvent -= UnsubscribeCoin;

        if (_subscribedCoins == null)
            return;

        foreach (Coin coin in _subscribedCoins)
            coin.OnCoinCollectedEvent -= HandleCoinCollected;

        _subscribedCoins.Clear();
    }

    private void SubscribeCoin(Coin coin)
    {
        coin.OnCoinCollectedEvent += HandleCoinCollected;
        _subscribedCoins.Add(coin);
    }

    private void UnsubscribeCoin(Coin coin)
    {
        coin.OnCoinCollectedEvent -= HandleCoinCollected;
        _subscribedCoins.Remove(coin);
    }

    private void HandleCoinCollected(Coin coin)
    {
        _coinCount++;
    }
}
