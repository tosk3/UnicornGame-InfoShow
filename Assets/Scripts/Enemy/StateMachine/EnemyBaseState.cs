using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    attack,
    patrol,
    die,
    chase,
    run
}
public abstract class EnemyBaseState : ScriptableObject
{
    [SerializeField] private List<EnemyBaseState> transitions;

    public virtual EnemyState StateType { get; }

    public List<EnemyBaseState> Transitions => transitions;

    public abstract bool ExitState(Enemy enemy,out EnemyState state);
    public abstract bool CheckRules(Enemy enemy);
    public abstract void ExecuteState(Enemy enemy);
}
