using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileConfig config;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private Rigidbody rb;

    public int Damage;
    public Player Source;

    private void Start()
    {
        rb.velocity = transform.forward * config.Speed;
        Invoke(nameof(DestroyProjectile), config.LifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player target))
        {
            if (target.IsPlayer == Source.IsPlayer)
                return;

            Instantiate(hitEffect, transform.position, Quaternion.identity);
            target.BeHit(Mathf.RoundToInt(config.DamageFactor * Damage));
            Destroy(gameObject);
        }
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
