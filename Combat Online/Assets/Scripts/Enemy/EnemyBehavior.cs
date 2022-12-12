using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private EnemyCombat combat;
    [SerializeField] private ExternalBehavior behavior;

    private SharedBool isDead;
    private SharedGameObjectList playerList;
    private SharedGameObjectList healthItems;
    private SharedGameObjectList damageItems;
    private SharedBool canUseSkill;
    private SharedBool canAttack;
    private SharedInt health;
    private SharedInt warningHealth;

    private bool ready = false;

    private BehaviorTree behaviorTree;

    private IEnumerator Start()
    {
        behaviorTree = gameObject.AddComponent<BehaviorTree>();
        behaviorTree.ExternalBehavior = behavior;
        yield return new WaitUntil(() => behaviorTree.isActiveAndEnabled);
        ready = true;

        isDead = behaviorTree.GetVariable("IsDead") as SharedBool;
        playerList = behaviorTree.GetVariable("PlayerList") as SharedGameObjectList;
        healthItems = behaviorTree.GetVariable("HealthItems") as SharedGameObjectList;
        damageItems = behaviorTree.GetVariable("DamageItems") as SharedGameObjectList;
        canUseSkill = behaviorTree.GetVariable("CanUseSkill") as SharedBool;
        canAttack = behaviorTree.GetVariable("CanAttack") as SharedBool;
        health = behaviorTree.GetVariable("Health") as SharedInt;
        warningHealth = behaviorTree.GetVariable("WarningHealth") as SharedInt;
        behaviorTree.RegisterEvent("Attack", DoAttack);
        behaviorTree.RegisterEvent("Skill", DoSkill);
        enemy.OnDead += () => isDead.Value = true;
        enemy.OnBeHit += () => behaviorTree.SendEvent("BeHit");

        canUseSkill.Value = true;
        canAttack.Value = true;
        warningHealth.Value = 50;
        health.Value = enemy.Health;
        isDead.Value = false;
    }

    private void DoAttack()
    {
        combat.Attack();
        StartCoroutine(DelayAttack());
    }

    private void Update()
    {
        if (!ready) return;

        playerList.Value = GameManager.Instance.Players;
        healthItems.Value = SpawnItemManager.Instance.HealthItems;
        damageItems.Value = SpawnItemManager.Instance.DamageItems;
        health.Value = enemy.Health;
    }

    IEnumerator DelayAttack()
    {
        canAttack.Value = false;
        yield return new WaitForSeconds(1.5f);
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
        yield return new WaitForSeconds(3f);
        canUseSkill.Value = true;
    }
}
