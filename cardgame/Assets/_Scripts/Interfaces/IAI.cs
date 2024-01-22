using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public interface IAIPlayer : IBuffable,IDamageable
{
    AIStateManager aiState {get;}
    public void Initialize(AIStateManager ai);

    public void BasicAttack(IAIPlayer playerBase);

    public void OnKillSomeone(IAIPlayer playerBase);
    public void OnGoing(IAIPlayer playerBase);
    public void OnReveal(IAIPlayer playerBase);
    public void OnCollision(IAIPlayer playerBase,Collision senderCollision);
}
public abstract class AIPlayerBase : MonoBehaviour, IAIPlayer , IBuffable, IDamageable
{
    public AIStateManager aiState => _aIState;
    private AIStateManager _aIState;
    public virtual void Initialize(AIStateManager ai)
    {
        _aIState = ai;
    }
    public abstract void OnHit(IAIPlayer playerBase,float senderAttackPow);
    public abstract void OnKillSomeone(IAIPlayer playerBase);
    public abstract void OnGoing(IAIPlayer playerBase);
    public  abstract void OnReveal(IAIPlayer playerBase);
    public abstract void BasicAttack(IAIPlayer playerBase);
    public abstract void OnCollision(IAIPlayer playerBase,Collision sendercol);
    public void onChangeAttackPower(float senderAttackPower, bool isBuff)
    {
        if(isBuff)
        {
            aiState.currentAttackPower += senderAttackPower;
        }
        else
        {
            aiState.currentAttackPower -= senderAttackPower;
        }
    }

    public void onChangeAttackSpeed(float senderAttackSpeed, bool isBuff)
    {
        if(isBuff)
        {
            aiState.currentAttackSpeed += senderAttackSpeed;
        }
        else
        {
            aiState.currentAttackSpeed -= senderAttackSpeed;
        }
    }

    public void onChangeHealth(float senderHealthvalue, bool isBuff)
    {
        if(isBuff)
        {
            if (aiState.currentHealth + senderHealthvalue < aiState.maxHealth)
            {
                aiState.currentHealth += senderHealthvalue;
                aiState.onDamage.Raise(this,new object[] {aiState.transform.position,senderHealthvalue});
            }
            else
            {
                aiState.onDamage.Raise(this,new object[] {aiState.transform.position,senderHealthvalue});
                aiState.currentHealth = aiState.maxHealth;
            }
        }
        else
        {
            aiState.currentHealth -= senderHealthvalue;
            aiState.onDamage.Raise(this, new object[] { aiState.transform.position, -senderHealthvalue });
        }
        aiState.aiWorldSpaceUI.UpdateUI();
    }

    public void onChangeMovementSpeed(float senderMovementvalue, bool isBuff)
    {
        if(isBuff)
        {
            aiState.currentMovementSpeed += senderMovementvalue;
        }
        else
        {
            aiState.currentMovementSpeed -= senderMovementvalue;
        }
    }

    public void TakeDamage(IAIPlayer senderAI,float senderDamage)
    {
        for (int i = 0; i < aiState.aiMaterial.Length; i++)
        {
            aiState.aiMaterial[i].material.SetFloat("_TextureImpact",0);
        }
        DOVirtual.DelayedCall(0.1f, () =>
        {
            for (int i = 0; i < aiState.aiMaterial.Length; i++)
            {
                aiState.aiMaterial[i].material.SetFloat("_TextureImpact",1);
            }
        });
        if (aiState.currentHealth - senderDamage <=0)
        {
            aiState.currentHealth = 0;
            Ded(senderAI);
        }
        else
        {
            aiState.currentHealth -= senderDamage;
            aiState.onDamage.Raise(this,new object[] {aiState.transform.position,-senderDamage});
        }
        aiState.aiWorldSpaceUI.UpdateUI();
    }
    public void Ded(IAIPlayer murderAI)
    {
        if (aiState.isEnemy)
        {
            ServiceLocator.Current.Get<EnemyManager>().EnemiesInDungeon.Remove(aiState.currentAIType);
            ServiceLocator.Current.Get<EnemyManager>().DeadEnemiesInDungeon.Add(aiState.currentAIType);
        }
        else
        {
            ServiceLocator.Current.Get<EnemyManager>().AlliesInDungeon.Remove(aiState.currentAIType);
            ServiceLocator.Current.Get<EnemyManager>().DeadAlliesInDungeon.Add(aiState.currentAIType);
        }
        aiState.isDead = true;
        aiState.animator.enabled = false;
        // aiState.onDead.Raise(this.aiState, new object[] { });
        aiState.GetComponent<Collider>().enabled = false;
        murderAI.OnKillSomeone(murderAI);
        if (aiState.AiRdStabilizer !=null)
        {
            aiState.AiRdStabilizer.StabilizePower = 0;
            aiState.AiRdStabilizer.HardForceStability = 0;
            aiState.AiRdAnimator.Parameters.RotateToPoseForce = 0;
        }
    }
    public void unDed()
    {
        if (aiState.isEnemy)
        {
            ServiceLocator.Current.Get<EnemyManager>().DeadEnemiesInDungeon.Remove(aiState.currentAIType);
            ServiceLocator.Current.Get<EnemyManager>().EnemiesInDungeon.Add(aiState.currentAIType);
        }
        else
        {
            ServiceLocator.Current.Get<EnemyManager>().DeadAlliesInDungeon.Remove(aiState.currentAIType);
            ServiceLocator.Current.Get<EnemyManager>().AlliesInDungeon.Add(aiState.currentAIType);
        }
        aiState.isDead = false;
        aiState.animator.enabled = true;
        aiState.GetComponent<Collider>().enabled = true;
        if (aiState.AiRdStabilizer !=null)
        {
            aiState.AiRdStabilizer.StabilizePower = 0.5f;
            aiState.AiRdStabilizer.HardForceStability = 0.33f;
            aiState.AiRdAnimator.Parameters.RotateToPoseForce = 0.8f;
        }
    }
}
