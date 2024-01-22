using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class Vampire : AIPlayerBase
{
    [Header("Events")]
    public GameEvent onBuff;
    public GameEvent onCardInit;

    public override void Initialize(AIStateManager ai)
    {
        base.Initialize(ai);
    }

    public override void OnHit(IAIPlayer playerBase,float senderAttackPow)
    {
        playerBase.onChangeHealth(senderAttackPow /2,true);
    }

    public override void OnKillSomeone(IAIPlayer playerBase)
    {
    }

    public override void BasicAttack(IAIPlayer playerBase)
    {
        Transform.Instantiate(playerBase.aiState.AttackOBJ, playerBase.aiState.AttackPosition.position,Quaternion.identity,playerBase.aiState.transform);
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