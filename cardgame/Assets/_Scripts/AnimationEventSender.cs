using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventSender : MonoBehaviour
{
    private AIStateManager AIManager;

    private int attackindex = 0;
    private void Start()
    {
        AIManager = transform.GetComponentInParent<AIStateManager>();
    }
    public void AnimationAttackEvent()
    {
        AIManager.animator.SetFloat("AttackSpeed", AIManager.currentAttackSpeed );
        AIManager.currentAIType.BasicAttack(AIManager.currentAIType);
    }
}
