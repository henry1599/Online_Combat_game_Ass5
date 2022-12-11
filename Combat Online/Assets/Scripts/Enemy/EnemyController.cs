using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;

public class EnemyController : PlayerController
{
    public bool IsMoveToward = false;
    [SerializeField] private NavMeshAgent agent;
    protected override void Start()
    {
        base.Start();

        agent.updatePosition = false;

    }

    protected override void Update()
    {
        base.Update();

        if (agent.remainingDistance <= agent.stoppingDistance && !agent.hasPath)
            movement = Vector2.zero;
    }

    protected override void GatherInput()
    {
        if (IsMoveToward) return;

        SetDestination(agent.nextPosition);
    }

    public void SetMoveToward(Vector3 destination)
    {
        SetDestination(destination);
    }

    private void SetDestination(Vector3 destination)
    {
        Vector3 delta = destination - transform.position;
        delta.y = 0;
        delta.Normalize();
        Debug.Log(delta);
        movement = new Vector2(Mathf.Abs(delta.x) > 0.001f ? -delta.x : 0, Mathf.Abs(delta.z) > 0.001f ? -delta.z : 0);
    }
}
