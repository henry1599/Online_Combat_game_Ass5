using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;

public class EnemyController : MonoBehaviour
{
    private static readonly int SpeedKeyAnimation = Animator.StringToHash("speed");
    [SerializeField] private Player player;
    [SerializeField] private Animator animator;
    private PlayerCombat combat;
    [SerializeField] private NavMeshAgent agent;
    private void Start()
    {
        combat = GetComponent<PlayerCombat>();
        player.OnBeHit += () => animator.SetTrigger("behit");
        player.OnDead += () => animator.SetTrigger("dead");
    }

    private void Update()
    {
        animator.SetFloat(SpeedKeyAnimation, agent.velocity.sqrMagnitude);
    }

    public void SetMoveToward(Vector3 destination)
    {
        SetDestination(destination);
    }

    private void SetDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
    }
}
