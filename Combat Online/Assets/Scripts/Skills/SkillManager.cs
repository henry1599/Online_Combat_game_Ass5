using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private GameObject muzzleEffect;

    private void Awake()
    {
        Instance = this;
    }

    public void DoSkill(Player player)
    {
        Vector3 position = player.transform.position;
        position.y = 1;
        Quaternion rotation = player.transform.rotation;
        Instantiate(muzzleEffect, position, rotation);
        Vector3 offset = player.transform.forward * 1.5f;
        position += offset;
        position.y = 1;
        int damage = player.Damage;
        CreateProjectile(position, rotation, damage, player);
    }

    public void CreateProjectile(Vector3 position, Quaternion rotation, int damage, Player source)
    {
        var projectile = Instantiate(projectilePrefab, position, rotation);
        projectile.Damage = damage;
        projectile.Source = source;
    }

}
