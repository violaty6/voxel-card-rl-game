using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class Dummy : AIPlayerBase
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
    }
    public override void OnReveal(IAIPlayer playerBase)
    {
        onCardInit.Raise(this,new object[] {playerBase.aiState.isEnemy});
    }
    public override void OnCollision(IAIPlayer receiverPlayerBase, Collision sendercol)
    {

    }
    public override void OnGoing(IAIPlayer playerBase)
    {

    }
}