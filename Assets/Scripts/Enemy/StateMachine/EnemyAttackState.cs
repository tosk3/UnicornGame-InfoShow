using System.Data;
using UnityEngine;

[CreateAssetMenu(menuName ="enemy/ai/state/attack")]
public class EnemyAttackState : EnemyBaseState
{
    [SerializeField] private float distance;
    public override EnemyState StateType 
    {
        get { return EnemyState.attack; } 
    }
   
    public override bool CheckRules(Enemy enemy)
    {
        float currentDistace = enemy.GetDistanceToPlayer();
        if (currentDistace <= distance)
        {
            return true;
        }
        return false;
    }

    public override void ExecuteState(Enemy enemy)
    {
        enemy.AttackTarget();
    }

    public override bool ExitState(Enemy enemy,out EnemyState state)
    {
        float currentDistace = enemy.GetDistanceToPlayer();

        if (enemy.GetHealth() <= 0)
        {
            state = EnemyState.die;
            return true;
        }

        if(currentDistace >= distance)
        {
            state = EnemyState.chase;
            return true;
        }
        state = EnemyState.attack;
        return false;    
           
    }
}
