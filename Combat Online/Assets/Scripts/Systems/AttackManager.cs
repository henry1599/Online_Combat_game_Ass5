using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public static AttackManager Instance { get; private set; }
    [SerializeField] private AttackManagerConfig config;
    private List<Player> players = new List<Player>();

    private List<Player> deadPlayers = new List<Player>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddPlayer(Player player)
    {
        players.Add(player);
        player.OnDead += () => deadPlayers.Add(player);
    }

    public void DoAttack(Player playerSource)
    {
        foreach (Player player in players)
        {
            if (ReferenceEquals(player, playerSource))
                continue;

            if (player.IsPlayer == playerSource.IsPlayer)
                continue;

            if (IsInAttackRange(playerSource, player))
            {
                player.BeHit(playerSource.Damage);
            }
        }
    }

    private void LateUpdate()
    {
        foreach (Player deadPlayer in deadPlayers)
        {
            players.Remove(deadPlayer);
        }
        deadPlayers.Clear();
    }

    private bool IsInAttackRange(Player playerSource, Player otherPlayer)
    {
        Vector3 direction = otherPlayer.transform.position - playerSource.transform.position;
        if (Vector3.Angle(direction, playerSource.transform.forward) > config.MaxAngle / 2)
            return false;
        float distance = direction.magnitude;
        if (distance > config.MaxDistance)
            return false;
        return true;
    }
}
