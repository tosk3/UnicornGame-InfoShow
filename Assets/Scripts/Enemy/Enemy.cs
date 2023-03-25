using System;
using System.Collections;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform playerTarget;
    [SerializeField] private float distaceToPlayer;

    [SerializeField] private float speed;
    [SerializeField] private float health;
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackTimer;
    [SerializeField] private float timerMax;
    [SerializeField] private float secondsToDie;

    [SerializeField] private EnemyBaseState currentState;
 

    // Update is called once per frame
    void Update()
    {
        CalculateDistanceToPlayer();     
        if (currentState.ExitState(this, out EnemyState state))
        {
            ChangeState(state);
        }
        if (currentState.CheckRules(this))
        {
            currentState.ExecuteState(this);
        }
    }

    private void ChangeState(EnemyState state)
    {
        for (int i = 0; i < currentState.Transitions.Count; i++)
        {
            Debug.Log(currentState.Transitions[i].StateType == state);

            if (currentState.Transitions[i].StateType == state)
            {
                currentState = currentState.Transitions[i];
                return;
            }
        }   
        //currentState = currentState.Transitions[currentState.Transitions.IndexOf(item => item.StateType == state)];
    }

    public Transform GetTarget()
    {
        return playerTarget;
    }
    public void SetTarget(Transform playerTarget)
    {
        if (this.playerTarget == null)
        {
            this.playerTarget = playerTarget;
        }
    }
    public void AttackTarget()
    {
        if (RunTimer())
        {
            playerTarget.GetComponent<PlayerController>().TakeDamage(attackDamage);
        }
    }
    private bool RunTimer()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            attackTimer = timerMax;
            return true;
        }
        return false;
    }

    public void MoveToTarget(float direction)
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTarget.position, direction * speed * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation((transform.position - playerTarget.position).normalized);
    } 
    public float GetDistanceToPlayer()
    {
        return distaceToPlayer;
    }
    public void CalculateDistanceToPlayer()
    {
        distaceToPlayer = Vector3.Distance(transform.position, GetTarget().position);
    }

    public float GetHealth()
    {
        return health;
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
    }
    public void Die()
    {
        StartCoroutine(Coroutine_Death());
    }
    private IEnumerator Coroutine_Death()
    {
        yield return new WaitForSeconds(secondsToDie);
        Destroy(gameObject);
    }
}
