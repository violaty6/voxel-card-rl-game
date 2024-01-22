using UnityEngine;

public class AI_StunState : AIBaseState
{

    public override void EnterState(AIStateManager ai)
    {
        ai.animator.SetBool("isStun", true);
    }

    public override void OnCollisionEnterState(AIStateManager ai, Collision col)
    {

    }

    public override void UpdateState(AIStateManager ai)
    {

    }
    public override void CheckEveryNSecond(AIStateManager ai, float second)
    {

    }
    public override void ExitState(AIStateManager ai)
    {
        ai.animator.SetBool("isStun", false);
    }
}
