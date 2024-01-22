using UnityEngine;
using UnityEngine.AI;

public class AI_ChaseState : AIBaseState
{
    public override void EnterState(AIStateManager ai)
    {
        ai.agent.updatePosition = true;
        ai.animator.SetBool("isWalking", true);
    }
    public override void OnCollisionEnterState(AIStateManager ai, Collision col)
    {
        
    }
    public override void UpdateState(AIStateManager ai)
    {
        ai.agent.SetDestination(ai.ChasingEnemy.transform.position);
        if (ai.agent.remainingDistance < ai.agent.stoppingDistance)
        {
            ai.SwitchState(ai.attackState);
        }
    }
    public override void CheckEveryNSecond(AIStateManager ai, float second)
    {

    }
    public override void ExitState(AIStateManager ai)
    {
        ai.animator.SetBool("isWalking", false);
    }

}
