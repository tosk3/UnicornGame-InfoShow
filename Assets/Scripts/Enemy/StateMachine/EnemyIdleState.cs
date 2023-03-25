using UnityEngine;

[CreateAssetMenu(menuName = "enemy/ai/state/idle")]
public class EnemyIdleState : EnemyBaseState
{
    public override EnemyState StateType
    {
        get { return EnemyState.idle; }
    }
    [SerializeField] private float idleDistance;
    [SerializeField] private EnemyState defaultOutOfIdleState;
    public override bool CheckRules(Enemy enemy)
    {
        float currentDistace = enemy.GetDistanceToPlayer();
        if (currentDistace > idleDistance)
        {
            return true;
        }
        return false;

    }

    public override void ExecuteState(Enemy enemy)
    {
       //do nothing or patrol ?
    }

    public override bool ExitState(Enemy enemy, out EnemyState state)
    {
        float currentDistace = enemy.GetDistanceToPlayer();

        if (enemy.GetHealth() <= 0)
        {
            state = EnemyState.die;
            return true;
        }
        if(currentDistace <= idleDistance)
        {
            state = defaultOutOfIdleState;
            return true;
        }
        state = EnemyState.idle;
        return false;
    }
}
