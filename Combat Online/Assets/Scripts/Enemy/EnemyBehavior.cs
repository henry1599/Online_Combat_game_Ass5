using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private BehaviorTree behaviorTree;
    [SerializeField] private EnemyCombat combat;

    private SharedBool isDead;
    private SharedGameObjectList playerList;
    private SharedBool canUseSkill;
    private SharedBool canAttack;
    private SharedInt health;
    private SharedInt warningHealth;

    private void Start()
    {
        isDead = behaviorTree.GetVariable("IsDead") as SharedBool;
        playerList = behaviorTree.GetVariable("PlayerList") as SharedGameObjectList;
        canUseSkill = behaviorTree.GetVariable("CanUseSkill") as SharedBool;
        canAttack = behaviorTree.GetVariable("CanAttack") as SharedBool;
        health = behaviorTree.GetVariable("Health") as SharedInt;
        warningHealth = behaviorTree.GetVariable("WarningHealth") as SharedInt;
        behaviorTree.RegisterEvent("Attack", DoAttack);
        behaviorTree.RegisterEvent("Skill", DoSkill);
        enemy.OnDead += () => isDead.Value = true;
    }

    private void DoAttack()
    {
        combat.Attack();
        StartCoroutine(DelayAttack());
    }

    private void Update()
    {
        playerList.Value = GameManager.Instance.Players;
        health.Value = enemy.Health;
    }

    IEnumerator DelayAttack()
    {
        canAttack.Value = false;
        yield return new WaitForSeconds(0.35f);
        canAttack.Value = true;
    }

    private void DoSkill()
    {
        combat.SpecialAttack();
        StartCoroutine(DelaySkill());
    }

    IEnumerator DelaySkill()
    {
        canUseSkill.Value = false;
        yield return new WaitForSeconds(1.95f);
        canUseSkill.Value = true;
    }
}
