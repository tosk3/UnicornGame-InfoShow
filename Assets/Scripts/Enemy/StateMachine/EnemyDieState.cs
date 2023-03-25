using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "enemy/ai/state/die")]
public class EnemyDieState : EnemyBaseState
{
    public override EnemyState StateType
    {
        get { return EnemyState.die; }
    }
    public override bool CheckRules(Enemy enemy)
    {
        if (enemy.GetHealth() <= 0)
        {
            return true;
        }  
        else
        {
            return false;
        }
    }

    public override void ExecuteState(Enemy enemy)
    {
        //timer death
        enemy.Die();
    }

    public override bool ExitState(Enemy enemy, out EnemyState state)
    {
        state = EnemyState.die;
        return false;
    }
}
