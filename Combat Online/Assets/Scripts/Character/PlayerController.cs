using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static readonly int SpeedKeyAnimation = Animator.StringToHash("speed");
    public Animator animator;
    public float moveSpeed;
    public float dampingRotation;
    Rigidbody rb;
    Vector2 movement;
    PlayerCombat combat;
    float angle;
    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        this.combat = GetComponent<PlayerCombat>();
    }
    // Update is called once per frame
    void Update()
    {
        if (this.combat.IsAttacking)
        {
            return;
        }
        GatherInput();
        this.animator.SetFloat(SpeedKeyAnimation, this.rb.velocity.sqrMagnitude);
        RotateTowardsVelocity();
    }
    void FixedUpdate()
    {
        if (this.combat.IsAttacking)
        {
            return;
        }
        this.rb.velocity = new Vector3(movement.normalized.x * this.moveSpeed * Time.fixedDeltaTime, rb.velocity.y, movement.normalized.y * this.moveSpeed * Time.fixedDeltaTime);
    }
    public void RotateTowardsVelocity()
    {
        if (this.movement.Equals(Vector2.zero))
            return;
        if (this.rb.velocity.Equals(Vector3.zero))
            return;
        Vector3 v = this.rb.velocity;
        this.angle = Mathf.Atan2(v.x, v.z) * Mathf.Rad2Deg;
        // transform.rotation = Quaternion.AngleAxis(this.angle, Vector3.forward);
        var desiredRotQ = Quaternion.Euler(transform.eulerAngles.x, this.angle, transform.eulerAngles.z);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotQ, Time.deltaTime * this.dampingRotation);
    }
    void GatherInput()
    {
        this.movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
