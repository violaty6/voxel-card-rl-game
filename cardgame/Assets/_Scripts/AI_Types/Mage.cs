using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class Mage : AIPlayerBase
{
    [Header("Events")]
    public GameEvent onAreaOpen; // Generate Area
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
        playerBase.aiState.animator.SetFloat("AttackSpeed", playerBase.aiState.currentAttackSpeed );
        Transform.Instantiate(playerBase.aiState.AttackOBJ, playerBase.aiState.AttackPosition.position,Quaternion.identity,playerBase.aiState.transform);
    }
    public override void OnReveal(IAIPlayer playerBase)
    {
        onAreaOpen.Raise(this, new object[] { 0 }); // 0 is circular 1 is rectangular 
        onCardInit.Raise(this,new object[] {this.aiState.isEnemy});
    }

    public override void OnCollision(IAIPlayer receiverPlayerBase, Collision sendercol)
    {
        
    }

    public override void OnGoing(IAIPlayer playerBase)
    {
        if (playerBase.aiState.isDead)
        {
            return;
        }
        if (!playerBase.aiState.isEnemy && !this.aiState.isEnemy)
        {
            playerBase.onChangeHealth(5,true);
        }
        else if (playerBase.aiState.isEnemy && this.aiState.isEnemy)
        {
            playerBase.onChangeHealth(5,true);
        }
    }
    private void OnDestroy()
    {
        
    }
}