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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent<Player>(out Player player))
            OnCoinCollected();
    }

    private void OnCoinCollected()
    {
        OnCoinCollectedEvent?.Invoke(this);
    }
}