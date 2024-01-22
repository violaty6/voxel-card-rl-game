using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedInformation : MonoBehaviour
{
    [SerializeField] GameObject SelectedInformationUIObj;
    [SerializeField] TextMeshProUGUI CardName;
    [SerializeField] Image CardImage;
    [SerializeField] TextMeshProUGUI AttackPower;
    [SerializeField] TextMeshProUGUI AttackRange;
    [SerializeField] TextMeshProUGUI AttackSpeed;
    [SerializeField] TextMeshProUGUI MovementSpeed;
    [SerializeField] private TextMeshProUGUI Health;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private AIStateManager SelectedAI;
    public void OpenSelectedInformationArea(Component sender, object data)
    {
        AIStateManager aiManager = sender as AIStateManager;
        if (aiManager != null)
        {
            SelectedAI = aiManager;
            SelectedInformationUIObj.SetActive(true);
            OpenFeedBackUI();
        }
        else
        {
            Debug.LogWarning("The sender is not of type Card.");
        }
    }
    public void CloseSelectedInformationArea()
    {
        CloseFeedBackUI();
        SelectedInformationUIObj.SetActive(false);
    }

    private void Update()
    {
        if (SelectedAI !=null)
        {
            SetSelectedInformation(SelectedAI.currentCard.cardName,SelectedAI.currentAttackPower,SelectedAI.currentAttackSpeed,SelectedAI.currentMovementSpeed,SelectedAI.currentCard.artwork,SelectedAI.currentHealth,SelectedAI.maxHealth,SelectedAI.currentAttackRange);
        }
    }

    private void SetSelectedInformation(string nameCard,float attackPower,float attackSpeed,float movementSpeed,Sprite cardsprite,float health,float maxhealth,float attckrange)
    {
        CardName.text = nameCard;
        AttackPower.text = attackPower.ToString();
        AttackSpeed.text = attackSpeed.ToString();
        MovementSpeed.text = movementSpeed.ToString();
        CardImage.sprite = cardsprite;
        healthSlider.maxValue = maxhealth;
        healthSlider.value = health;
        AttackRange.text = attckrange.ToString();
        Health.text = health.ToString()+"/"+maxhealth ;
    }
    private void OpenFeedBackUI()
    {
        SelectedInformationUIObj.transform.localScale = Vector3.one / 2;
        SelectedInformationUIObj.transform.DOScale(1, 0.3f);
    }
    private void CloseFeedBackUI()
    {
        SelectedInformationUIObj.transform.DOScale(Vector3.one / 2, 0.3f);
    }
}
