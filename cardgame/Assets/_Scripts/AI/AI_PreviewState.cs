using UnityEngine;

public class AI_PreviewState : AIBaseState
{
    public override void EnterState(AIStateManager ai)
    {
        ai.animator.SetBool("isPreview", true);
        ai.ragdollTowards.transform.position += Vector3.up;
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
        ai.ragdollTowards.transform.position -= Vector3.up/1.02f;
        ai.animator.SetBool("isPreview", false);
    }
}
