using System;
using System.Collections;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public event EventHandler<OnDeathArgs> OnEnemyDeath;
    public class OnDeathArgs : EventArgs
    {
        public Vector3 position;
    }
    [SerializeField] private Transform playerTarget;
    [SerializeField] private float distaceToPlayer;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float speed;
    [SerializeField] private float health;
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackTimer;
    [SerializeField] private float timerMax;
    [SerializeField] private float secondsToDie;
    [SerializeField] private MagicMissleGun gun;

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
            if(gun != null)
            {
                gun.Shoot(playerTarget.position);
            }
            else
            {
                playerTarget.GetComponentInParent<PlayerController>().TakeDamage(attackDamage); 
            }
           
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
        rb.isKinematic = false;
        OnEnemyDeath?.Invoke(this, new OnDeathArgs() { position = this.transform.position });
        StartCoroutine(Coroutine_Death());
    }
    private IEnumerator Coroutine_Death()
    {
        yield return new WaitForSeconds(secondsToDie);
        Destroy(gameObject);
    }
}
