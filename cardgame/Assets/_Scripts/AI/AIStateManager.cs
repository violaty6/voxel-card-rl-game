using System;
using System.Collections;
using System.Collections.Generic;
using FIMSpace.FProceduralAnimation;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AIStateManager : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float currentAttackPower;
    public float currentAttackSpeed;
    public float currentMovementSpeed;
    public float currentAttackRange;
    public GameObject AttackOBJ;
    public Card currentCard;
    public AIPlayerBase currentAIType;
    public NavMeshAgent agent;
    public AIStateManager ChasingEnemy;
    public GameObject ragdollTowards;
    public Animator animator;
    public AnimatorOverrideController animatorOverrideController;
    public bool isFirst = false;
    public bool isEnemy = false;
    public bool isDead = false;
    public UIGetInformation aiWorldSpaceUI;

    [Header("Events")]
    public GameEvent onPreview;
    public GameEvent onDamage;
    public GameEvent onDead;
    
    AIBaseState currentState;
    #region States
    public AI_PreviewState previewState = new AI_PreviewState();
    public AI_IdleState idleState = new AI_IdleState(); 
    public AI_AttackState attackState = new AI_AttackState();
    public AI_StunState stunState = new AI_StunState();
    public AI_ChaseState chaseState = new AI_ChaseState();
    #endregion
    [SerializeField]private TextMeshPro StateDebug;
    [SerializeField] private bool isRagdoll;
    public RagdollAnimatorStabilizer AiRdStabilizer;
    public RagdollAnimator AiRdAnimator;
    [SerializeField]public SkinnedMeshRenderer[] aiMaterial;
    public Transform AttackPosition;
    

    private void Start()
    {
        DefineCardToAI();
        agent = GetComponent<NavMeshAgent>();
        if (isRagdoll == true)
        {
            AiRdStabilizer = GetComponentInChildren<RagdollAnimatorStabilizer>();
            AiRdAnimator = GetComponentInChildren<RagdollAnimator>();  
        }
        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        currentAIType = GetComponent<AIPlayerBase>();
        currentAIType.Initialize(this);
        agent.stoppingDistance = currentAttackRange / 2;
        agent.speed = currentMovementSpeed/12;
        aiMaterial = transform.GetComponentsInChildren<SkinnedMeshRenderer>();
        DefineOutlineToAI();
        if(isEnemy)
        {
        currentState = idleState;
        currentAIType.Initialize(this);
        }
        else
        {
        currentState = previewState;
        }
        currentState.EnterState(this);
    }
    private void Update()
    {
        if (isDead) return;
        currentState.UpdateState(this);
        StateDebug.text = currentState.ToString();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isDead) return;
        // Debug.Log("OnColEnter---" + " AI Current State: " + currentState.ToString() + "---- " + " Receiver: " +
        //           collision.transform.gameObject + "---- " + "Sender: " + transform.GetComponent<Collider>());
        currentState.OnCollisionEnterState(this, collision);
    }
    public void SwitchState(AIBaseState state)
    {
        if (isDead) return;
        currentState.ExitState(this);
        currentState = state;
        state.EnterState(this);
    }
    public void ChangeAttackOverride(AnimatorOverrideController controller)
    {
            animator.runtimeAnimatorController = controller;
    }
    private void DefineCardToAI()
    {
        isRagdoll = currentCard.isRagdoll;
        currentAttackPower = currentCard.attack;
        currentAttackSpeed = currentCard.attackSpeed;
        maxHealth = currentCard.health;
        currentHealth = currentCard.health;
        currentMovementSpeed = currentCard.movementspeed;
        currentAttackRange = currentCard.AttackRange;
        AttackOBJ = currentCard.AttackPrefab;
        if (isEnemy)
        {
            transform.gameObject.layer = LayerMask.NameToLayer("Enemy");
        }
        else
        {
            transform.gameObject.layer = LayerMask.NameToLayer("Allie");
        }
        if ((currentCard.isHumanoid) && currentCard.AnimationOverrider !=null)
        {
            ChangeAttackOverride(currentCard.AnimationOverrider);
        }
    }

    private void DefineOutlineToAI()
    {
       if (isEnemy)
       {
           Outline outline = transform.AddComponent<Outline>();
           outline.OutlineMode = Outline.Mode.OutlineVisible;
           outline.OutlineWidth = 2;
           outline.OutlineColor = Color.red;
       }
    }
}
