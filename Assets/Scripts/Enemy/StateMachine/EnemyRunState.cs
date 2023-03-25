using UnityEngine;

[CreateAssetMenu(menuName = "enemy/ai/state/run")]
public class EnemyRunState : EnemyBaseState
{
    [SerializeField] private Vector2 minMaxRunDistance;
    public override EnemyState StateType
    {
        get { return EnemyState.run; }
    }
    public override bool CheckRules(Enemy enemy)
    {
        float currentDistace = enemy.GetDistanceToPlayer();

        if (currentDistace >= minMaxRunDistance.x)
        {
            return true;
        }
        return false;
    }

    public override void ExecuteState(Enemy enemy)
    {
        enemy.MoveToTarget(-1f);
    }

    public override bool ExitState(Enemy enemy, out EnemyState state)
    {
        float currentDistace = enemy.GetDistanceToPlayer();

        if (enemy.GetHealth() <= 0)
        {
            state = EnemyState.die;
            return true;
        }
  
        if(currentDistace >= minMaxRunDistance.y)
        {
            state = EnemyState.idle;
            return true;
        }
        if (currentDistace <= minMaxRunDistance.y)
        {
            state = EnemyState.chase;
            return true;
        }

        state = EnemyState.run;
        return false;
    }
}
