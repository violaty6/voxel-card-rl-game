using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName ="New Card", menuName ="Card")]
public class Card : ScriptableObject
{
    public string cardName;
    public string desc;
    public GameObject previewPrefab;
    public GameObject AttackPrefab;
    public AnimatorOverrideController AnimationOverrider;
    public bool isRagdoll;
    public bool isHumanoid;

    public Sprite artwork;

    public int manaCost;
    public float attack;
    public float attackSpeed;
    public float health;
    public float movementspeed;
    public float AttackRange;
}
