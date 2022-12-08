using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] KeyCode attackKey = KeyCode.C;
    [SerializeField] KeyCode specialAttackKey = KeyCode.V;
    [SerializeField] Animator animator;
    [SerializeField] float resetValue;
    public bool IsAttacking {get; set;}
    private static readonly int[] AttackKeysAnimation = new int[3]
    {
        Animator.StringToHash("Attack01"),
        Animator.StringToHash("Attack02"),
        Animator.StringToHash("Attack03")
    };
    private static readonly int SpecialAttackkeyAnimation = Animator.StringToHash("Push");
    private int currentAttackIdx
    {
        get => attackIdx;
        set
        {
            attackIdx = value;
            attackIdx %= AttackKeysAnimation.Length;

            animator.CrossFade(AttackKeysAnimation[attackIdx], 0, 0);
            IsAttacking = true;
        }
    } int attackIdx = -1;
    // Update is called once per frame
    void Update()
    {
        GetInput();
    }
    void GetInput()
    {
        if (Input.GetKeyDown(this.attackKey))
        {
            Attack();
        }
        if (Input.GetKeyDown(this.specialAttackKey))
        {
            SpecialAttack();
        }

    }
    void Attack()
    {
        if (IsAttacking)
            return;

        this.currentAttackIdx++;
        CancelInvoke(nameof(ResetAttackCombo));
        Invoke(nameof(ResetAttackCombo), this.resetValue);
    }
    void SpecialAttack()
    {
        if (IsAttacking)
            return;
        IsAttacking = true;
        ResetAttackCombo();
        this.animator.CrossFade(SpecialAttackkeyAnimation, 0, 0);
    }
    void ResetAttackCombo()
    {
        this.attackIdx = -1;
        IsAttacking = false;
    }
}
