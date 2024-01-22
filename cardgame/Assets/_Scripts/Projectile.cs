using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Projectile : MonoBehaviour
{
    private Sequence AttackSeq;
    private bool isProjectile = true;
    public AIStateManager ProjectileOwner;
    private void Start()
    {
        float time = 0.2f;
        ProjectileOwner = transform.GetComponentInParent<AIStateManager>();
        if (isProjectile)
        {
            Vector3 dir = ProjectileOwner.ChasingEnemy.transform.position - transform.position;
            dir = dir.normalized;
            transform.GetComponent<Rigidbody>().AddForce(dir *15,ForceMode.Impulse);
            transform.GetComponent<Rigidbody>().AddTorque(dir ,ForceMode.Impulse);
            time = 5f;
        }
        AttackSeq = DOTween.Sequence();
        AttackSeq.Join(DOVirtual.DelayedCall(time, () =>
        {
         Destroy(gameObject);
        }).SetLoops(-1));
    }

    private void OnCollisionEnter(Collision other)
    {
        AIStateManager ai = other.transform.GetComponent<AIStateManager>();
        if (ai !=null)
        {
            ai.currentAIType.TakeDamage(ProjectileOwner.currentAIType,ProjectileOwner.currentAttackPower);
            ProjectileOwner.currentAIType.OnHit(ProjectileOwner.currentAIType,ProjectileOwner.currentAttackPower);
            Destroy(transform.gameObject);
        }
    }

    private void OnDisable()
    {
        AttackSeq.Kill();
    }
}
