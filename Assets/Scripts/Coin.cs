using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public event Action<Coin> OnCoinCollectedEvent;

    public int SpawnPositionIndex { get; private set; }

    public void SetSpawnPositionIndex(int index)
    {
        SpawnPositionIndex = index;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            OnCoinCollected();
    }

    private void OnCoinCollected()
    {
        OnCoinCollectedEvent?.Invoke(this);
    }
}