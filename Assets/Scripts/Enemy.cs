using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform playerTarget;
    [SerializeField] private float distaceToPlayer;

    [SerializeField] private float speed;
    [SerializeField] private float health;
    [SerializeField] private float attackDamage;

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
    public void AttackTarget()
    {
        playerTarget.GetComponent<PlayerController>().TakeDamage(attackDamage);
    }

    internal float GetHealth()
    {
        return health;
    }

    public void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTarget.position, speed * Time.deltaTime);
    }

    public void Die()
    {
        this.gameObject.SetActive(false);
    }
    public float GetDistanceToPlayer()
    {
        return distaceToPlayer;
    }
    public void CalculateDistanceToPlayer()
    {
        distaceToPlayer = Vector3.Distance(transform.position, GetTarget().position);
    }
}
