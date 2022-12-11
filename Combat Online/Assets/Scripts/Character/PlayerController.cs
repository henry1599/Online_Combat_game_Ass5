using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static readonly int SpeedKeyAnimation = Animator.StringToHash("speed");
    [SerializeField] private Player player;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float dampingRotation;
    protected Vector2 movement;
    private Rigidbody rb;
    private PlayerCombat combat;
    private float angle;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        combat = GetComponent<PlayerCombat>();
        player.OnBeHit += () => animator.SetTrigger("behit");
        player.OnDead += () => animator.SetTrigger("dead");
    }

    protected virtual void Update()
    {
        if (combat.IsAttacking || player.IsStun || player.IsDead)
        {
            movement = Vector2.zero;
        }
        else
        {
            GatherInput();
        }
        animator.SetFloat(SpeedKeyAnimation, rb.velocity.sqrMagnitude);
        RotateTowardsVelocity();
    }

    private void FixedUpdate()
    {
        if (combat.IsAttacking || player.IsStun || player.IsDead)
        {
            rb.velocity = Vector3.zero;
            return;
        }
        rb.velocity = new Vector3(-movement.normalized.x * moveSpeed * Time.fixedDeltaTime, rb.velocity.y, -movement.normalized.y * moveSpeed * Time.fixedDeltaTime);
    }

    public void RotateTowardsVelocity()
    {
        if (movement.Equals(Vector2.zero))
            return;
        if (rb.velocity.Equals(Vector3.zero))
            return;
        Vector3 v = rb.velocity;
        angle = Mathf.Atan2(v.x, v.z) * Mathf.Rad2Deg;
        // transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        var desiredRotQ = Quaternion.Euler(transform.eulerAngles.x, angle, transform.eulerAngles.z);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotQ, Time.deltaTime * dampingRotation);
    }

    protected virtual void GatherInput()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
