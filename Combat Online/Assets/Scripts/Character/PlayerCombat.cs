using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour, IAttack, ISkill
{
    [SerializeField] Player player;
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
    }
    private int attackIdx = -1;

    private void Update()
    {
        if (player.IsStun || player.IsDead) return;

        GetInput();
    }

    protected virtual void GetInput()
    {
        if (Input.GetKeyDown(attackKey))
        {
            Attack();
        }
        if (Input.GetKeyDown(specialAttackKey))
        {
            SpecialAttack();
        }

    }

    public void Attack()
    {
        if (IsAttacking)
            return;

        currentAttackIdx++;
        CancelInvoke(nameof(ResetAttackCombo));
        Invoke(nameof(ResetAttackCombo), resetValue);
        DoAttack();
    }

    public void SpecialAttack()
    {
        if (IsAttacking)
            return;
        IsAttacking = true;
        ResetSpecialAttack();
        animator.CrossFade(SpecialAttackkeyAnimation, 0, 0);
        DoSkill();
    }

    private void ResetSpecialAttack()
    {
        attackIdx = -1;
    }

    private void ResetAttackCombo()
    {
        attackIdx = -1;
        IsAttacking = false;
    }

    public void DoAttack()
    {
        AttackManager.Instance.DoAttack(player);
    }

    public void DoSkill()
    {
        SkillManager.Instance.DoSkill(player);
    }
}
