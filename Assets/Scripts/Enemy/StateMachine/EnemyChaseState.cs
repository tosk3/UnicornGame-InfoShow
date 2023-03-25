using UnityEngine;

[CreateAssetMenu(menuName = "enemy/ai/state/chase")]
public class EnemyChaseState : EnemyBaseState
{
    [SerializeField] private Vector2 minMaxDistance;
    public override EnemyState StateType
    {
        get { return EnemyState.chase; }
    }
    public override bool CheckRules(Enemy enemy)
    {
        float currentDistace = enemy.GetDistanceToPlayer();
        if (currentDistace >= minMaxDistance.x && currentDistace <= minMaxDistance.y)
        {
            return true;
        }
        return false;
    }

    public override void ExecuteState(Enemy enemy)
    {
        enemy.MoveToTarget();
    }

    public override bool ExitState(Enemy enemy, out EnemyState state)
    {
        float currentDistace = enemy.GetDistanceToPlayer();

        if (enemy.GetHealth() <= 0)
        {
            state = EnemyState.die;
            return true;
        }

        if (currentDistace >= minMaxDistance.y)
        {
            state = EnemyState.idle;
            return true;
        }
        if (currentDistace < minMaxDistance.x)
        {
            state = EnemyState.attack;
            return true;
        }
        state = EnemyState.chase;
        return false;
    }

}
