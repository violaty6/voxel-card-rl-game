using UnityEngine;

public class AI_IdleState : AIBaseState
{
    public override void EnterState(AIStateManager ai)
    {
        ai.animator.SetBool("isIdle", true);
        if (!ai.isFirst)
        {
            ai.currentAIType.OnReveal(ai.currentAIType);    
            ai.isFirst = true;
        }
    }
    public override void OnCollisionEnterState(AIStateManager ai, Collision col)
    {
        
    }
    public override void UpdateState(AIStateManager ai)
    {
        if (ai.isEnemy)
        {
            if (ServiceLocator.Current.Get<EnemyManager>().AlliesInDungeon.Count<=0)
            {
                return;
            }
            else
            {
                float distance =500;
                for (int i = 0; i < ServiceLocator.Current.Get<EnemyManager>().AlliesInDungeon.Count; i++)
                {
                    float distanceai = Vector3.Distance(ServiceLocator.Current.Get<EnemyManager>().AlliesInDungeon[i].transform.position,ai.transform.position);
                    if (distance > distanceai)
                    {
                        distance = distanceai;
                        ai.ChasingEnemy = ServiceLocator.Current.Get<EnemyManager>().AlliesInDungeon[i].aiState;
                    }
                }
                ai.SwitchState(ai.chaseState);
            }   
        }
        else
        {
            if (ServiceLocator.Current.Get<EnemyManager>().EnemiesInDungeon.Count<=0)
            {
                return;
            }
            else
            {
                float distance =500;
                for (int i = 0; i < ServiceLocator.Current.Get<EnemyManager>().EnemiesInDungeon.Count; i++)
                {
                    float distanceai = Vector3.Distance(ServiceLocator.Current.Get<EnemyManager>().EnemiesInDungeon[i].transform.position,ai.transform.position);
                    if (distance > distanceai)
                    {
                        distance = distanceai;
                        ai.ChasingEnemy = ServiceLocator.Current.Get<EnemyManager>().EnemiesInDungeon[i].aiState;
                    }
                }
                ai.SwitchState(ai.chaseState);
            }   
        }

    }
    public override void CheckEveryNSecond(AIStateManager ai, float second)
    {

    }
    public override void ExitState(AIStateManager ai)
    {
        ai.animator.SetBool("isIdle", false);
    }
}
