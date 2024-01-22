using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICardInformation : MonoBehaviour
{
    [SerializeField] GameObject CardInformationUIObj;

    [SerializeField] GameObject CardObj;

    [Header("Events")]
    public GameEvent onCardInformationOpen;
    public GameEvent onCardInformationClose;

    [SerializeField] TextMeshProUGUI CardName;
    [SerializeField] TextMeshProUGUI CardDescription;
    [SerializeField] Image CardImage;
    [SerializeField] TextMeshProUGUI ManaCost;
    [SerializeField] TextMeshProUGUI AttackPower;
    [SerializeField] TextMeshProUGUI AttackRange;
    [SerializeField] TextMeshProUGUI AttackSpeed;
    [SerializeField] TextMeshProUGUI Health;
    [SerializeField] TextMeshProUGUI MovementSpeed; 

    public void OpenCardInformationArea(Component sender,object data)
    {
        CardDisplay cardDisplay = sender as CardDisplay;
        if (cardDisplay != null)
        {
            // The sender is a Card, proceed with displaying card information
            onCardInformationOpen.Raise(this, null);
            CardInformationUIObj.SetActive(true);
            OpenFeedBackUI();
            SetCardInformation(cardDisplay.card);
        }
        else
        {
            Debug.LogWarning("The sender is not of type Card.");
        }
    }
    public void CloseCardInformationArea()
    {
        CloseFeedBackUI();
        onCardInformationClose.Raise(this,null);
        CardInformationUIObj.SetActive(false);
    }
    private void SetCardInformation(Card card)
    {
        CardName.text = card.cardName;
        CardDescription.text = card.desc;
        CardImage.sprite = card.artwork;
        ManaCost.text = card.manaCost.ToString();
        AttackPower.text = card.attack.ToString();
        AttackRange.text = card.AttackRange.ToString();
        AttackSpeed.text = card.attackSpeed.ToString();
        Health.text = card.health.ToString();
        MovementSpeed.text = card.movementspeed.ToString();
    }
    private void OpenFeedBackUI()
    {
        CardObj.transform.localScale = Vector3.one/2;
        CardObj.transform.DOScale(1, 0.3f);
        // InformationObj.transform.localScale = Vector3.one / 2;
        // InformationObj.transform.DOScale(1, 0.3f);
    }
    private void CloseFeedBackUI()
    {
        CardObj.transform.DOScale(Vector3.one / 2, 0.3f);
        // InformationObj.transform.DOScale(Vector3.one / 2, 0.3f);
    }
}
