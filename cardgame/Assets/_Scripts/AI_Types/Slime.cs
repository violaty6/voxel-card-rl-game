using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class Slime : AIPlayerBase
{
    [Header("Events")]
    public GameEvent onBuff;
    public GameEvent onCardInit;

    public override void Initialize(AIStateManager ai)
    {
        base.Initialize(ai);
    }

    public override void OnHit(IAIPlayer playerBase, float senderAttackPow)
    {
        
    }

    public override void OnKillSomeone(IAIPlayer playerBase)
    {
        
    }

    public override void BasicAttack(IAIPlayer playerBase)
    {
        playerBase.aiState.agent.updatePosition = false;
        Vector3 dir = playerBase.aiState.ChasingEnemy.transform.position - transform.position;
        dir = dir.normalized;
        playerBase.aiState.GetComponent<Rigidbody>().AddForce(dir*100,ForceMode.Impulse);
    }
    public override void OnReveal(IAIPlayer playerBase)
    {
        onCardInit.Raise(this,new object[] {playerBase.aiState.isEnemy});
    }
    public override void OnCollision(IAIPlayer receiverPlayerBase, Collision sendercol)
    {
        AIStateManager senderAI = sendercol.transform.GetComponent<AIStateManager>();
        if (senderAI != null)
        {
            senderAI.currentAIType.TakeDamage(receiverPlayerBase,receiverPlayerBase.aiState.currentAttackPower);
        }
    }
    public override void OnGoing(IAIPlayer playerBase)
    {

    }
}