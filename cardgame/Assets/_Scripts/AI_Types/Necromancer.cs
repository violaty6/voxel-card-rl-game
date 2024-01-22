using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Necromancer : AIPlayerBase
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
        playerBase.aiState.animator.SetFloat("AttackSpeed", playerBase.aiState.currentAttackSpeed );
        Transform.Instantiate(playerBase.aiState.AttackOBJ, playerBase.aiState.AttackPosition.position,Quaternion.identity,playerBase.aiState.transform);
    }
    AIPlayerBase nearestAI;
    public override void OnReveal(IAIPlayer playerBase)
    {
        onCardInit.Raise(this,new object[] {playerBase.aiState.isEnemy});
        float distance =500;
        if (aiState.isEnemy)
        {
            for (int i = 0; i < ServiceLocator.Current.Get<EnemyManager>().DeadEnemiesInDungeon.Count; i++)
            {
                
                float distanceai = Vector3.Distance(ServiceLocator.Current.Get<EnemyManager>().DeadEnemiesInDungeon[i].transform.position,aiState.transform.position);
                if (distance > distanceai)
                {
                    distance = distanceai;
                    nearestAI = ServiceLocator.Current.Get<EnemyManager>().DeadEnemiesInDungeon[i];
                }
            }

            if (nearestAI != null)
            {
                nearestAI.unDed();
                nearestAI.onChangeHealth((nearestAI.aiState.maxHealth/100)*40,true);
            }
            nearestAI = null;
        }
        else
        {
            for (int i = 0; i < ServiceLocator.Current.Get<EnemyManager>().DeadAlliesInDungeon.Count; i++)
            {
                
                float distanceai = Vector3.Distance(ServiceLocator.Current.Get<EnemyManager>().DeadAlliesInDungeon[i].transform.position,aiState.transform.position);
                if (distance > distanceai)
                {
                    distance = distanceai;
                    nearestAI = ServiceLocator.Current.Get<EnemyManager>().DeadAlliesInDungeon[i];
                }
            }

            if (nearestAI != null)
            {
                nearestAI.unDed();
                nearestAI.onChangeHealth((nearestAI.aiState.maxHealth/100)*40,true);
            }
            nearestAI = null;
        }
    }

    public override void OnCollision(IAIPlayer senderplayerBase, Collision sendercol)
    {
        
    }

    public override void OnGoing(IAIPlayer playerBase)
    {

    }
}