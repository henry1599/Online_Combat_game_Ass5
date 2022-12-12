using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour, IBeHit
{
    [SerializeField] private Attribute attribute;

    public int Damage => attribute.Damage;
    public int Health => attribute.Health;

    public bool IsStun { get; private set; }
    public bool IsDead { get; private set; }
    public Action OnBeHit;
    public Action OnDead;
    public Action<int> OnHealth;

    private Coroutine increaseDamageCoroutine;
    private Coroutine stunCoroutine;

    public virtual bool IsPlayer => true;

    protected virtual IEnumerator Start()
    {
        yield return null;
        attribute.Init();
        OnHealth += attribute.IncreaseHealth;
        AttackManager.Instance.AddPlayer(this);
        if (IsPlayer)
        {
            GameManager.Instance.AddPlayer(gameObject);
            OnDead += () => GameManager.Instance.RemovePlayer(gameObject);
        }
    }

    public void IncreaseHealth(int amount)
    {
        OnHealth?.Invoke(amount);
    }

    public void IncreaseDamage(int amount, float time)
    {
        attribute.IncreaseDamage(amount);
        if (increaseDamageCoroutine != null)
            StopCoroutine(increaseDamageCoroutine);
        increaseDamageCoroutine = StartCoroutine(CountDownIncreaseDamage(time));
    }

    private IEnumerator CountDownIncreaseDamage(float time)
    {
        yield return new WaitForSeconds(time);
        attribute.DecreaseDamage();
    }

    public void BeHit(int damage)
    {
        LogBeHit(damage);
        attribute.DecreaseHealth(damage);
        if (attribute.Health <= 0)
        {
            IsDead = true;
            GetComponent<Collider>().enabled = false;
            OnDead?.Invoke();
        }
        else
        {
            if (stunCoroutine != null)
                StopCoroutine(stunCoroutine);
            stunCoroutine = StartCoroutine(Stun(IBeHit.StunTime));
            OnBeHit?.Invoke();
        }
    }

    private IEnumerator Stun(float time)
    {
        IsStun = true;
        yield return new WaitForSeconds(time);
        IsStun = false;
    }

    public void DoSkill()
    {
        throw new NotImplementedException();
    }

    public virtual void LogBeHit(int damge)
    {
        Debug.Log("Plaeyr take damage: " + damge);
    }
}
