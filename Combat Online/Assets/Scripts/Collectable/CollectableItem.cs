using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollectableItem : MonoBehaviour
{
    [SerializeField] private GameObject vfxCollected;
    public Action OnCollected;
    private void Update()
    {
        transform.Rotate(0, 120 * Time.deltaTime, 0);
    }
    public virtual void OnItemCollect(Player player)
    {
        Instantiate(vfxCollected, transform.position, Quaternion.identity);
        OnCollected?.Invoke();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<Player>(out Player player))
                OnItemCollect(player);
        }
    }
}
