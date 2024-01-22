using UnityEngine;
using DG.Tweening;

public class AI_AttackState : AIBaseState
{
    private Sequence AttackSeq;
    public override void EnterState(AIStateManager ai)
    {
        ai.animator.SetFloat("AttackSpeed", ai.currentAttackSpeed);
        ai.animator.SetBool("isAttack", true);
        // AttackSeq = DOTween.Sequence();
        // AttackSeq.Join(DOVirtual.DelayedCall(ai.currentAttackSpeed, () =>
        // {
        //   Attack(ai);
        // }).SetLoops(-1));
    }
    public override void OnCollisionEnterState(AIStateManager ai, Collision col)
    {
        ai.currentAIType.OnCollision(ai.currentAIType,col);
    }
    public override void UpdateState(AIStateManager ai)
    {
        ai.transform.LookAt(ai.ChasingEnemy.transform.position);
        if (ai.ChasingEnemy.isDead)
        {
            ai.SwitchState(ai.idleState);
        }
        if (ai.agent.remainingDistance > ai.agent.stoppingDistance)
        {
            ai.SwitchState(ai.chaseState);
        }
    }
    public override void CheckEveryNSecond(AIStateManager ai, float second)
    {

    }
    public override void ExitState(AIStateManager ai)
    {
        AttackSeq.Kill();
        ai.animator.SetBool("isAttack", false);
    }
}
