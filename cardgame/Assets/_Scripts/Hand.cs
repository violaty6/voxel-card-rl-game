using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public int startCardAmount = 4;
    public float startAngle = 0f;
    public float endAngle = 90f;
    public float xRadius;
    public float yRadius;
    public Collider2D cardToPreviewTreshold;

    public float CardsLookAtYDistance = 200f;

    public Deck DeckInfo;
    public List<GameObject> CurHand;
    public List<Card> currentDeck;
    public GameObject cardPrefab;

    private void Awake()
    {
        DOTween.Init();
        currentDeck = DeckInfo.DeckCards.ToList();
    }
    private void Start()
    {
        StartHand();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            DrawCard();
            ArrangeCardsInCircle(CurHand);
        }
    }
    private void ArrangeCardsInCircle(List<GameObject> cards)
    {
        if(CurHand.Count ==0) return;
        float lastEndAngle = endAngle + (CurHand.Count * 12);
        float lastStartAngle = startAngle - (CurHand.Count * 12);
        float angleIncrement = (lastEndAngle - lastStartAngle) / (CurHand.Count);
        for (int i = 0; i < CurHand.Count; i++)
        {
            float angle = lastStartAngle + i * angleIncrement;
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * xRadius;
            float y = Mathf.Sin(Mathf.Deg2Rad * angle) * yRadius;
            Vector3 cardPosition = new Vector3( + x,  + y,  + 0f);
            Quaternion cardRotation = Quaternion.LookRotation(Vector3.forward, cardPosition - (Vector3.down * CardsLookAtYDistance));
            CardDisplay cardDisplayComponent = cards[i].GetComponent<CardDisplay>();
            cardDisplayComponent.cardHandLocation = cardPosition;
            cardDisplayComponent.cardHandRotation = cardRotation;
            cardDisplayComponent.MoveToLocation();
        }
    }
    public static Vector3 getRelativePosition(Transform origin, Vector3 position) {
        Vector3 distance = position - origin.position;
        Vector3 relativePosition = Vector3.zero;
        relativePosition.x = Vector3.Dot(distance, origin.right.normalized);
        relativePosition.y = Vector3.Dot(distance, origin.up.normalized);
        relativePosition.z = Vector3.Dot(distance, origin.forward.normalized);
	
        return relativePosition;
    }
    private void StartHand()
    {
        for (int i = 0; i < startCardAmount; i++)
        {
            DrawCard();
        }
        ArrangeCardsInCircle(CurHand);
    }

    public void ThrowCard(GameObject card)
    {
        CurHand.Remove(card);

        Destroy(card);
        ArrangeCardsInCircle(CurHand);
    }
    public void DrawCard()
    {
        int randomIndex = Random.Range(0, currentDeck.Count);
        GameObject instiatedObj = Instantiate(cardPrefab, transform.position,Quaternion.identity);
        instiatedObj.transform.SetParent(transform);
        CardDisplay cardDisplay = instiatedObj.GetComponent<CardDisplay>();
        cardDisplay.CurHand = this.GetComponent<Hand>();
        cardDisplay.card = currentDeck[randomIndex];
        CurHand.Add(instiatedObj);
        currentDeck.Remove(currentDeck[randomIndex]);
        cardDisplay.SetDisplay();
    }
    private void OnApplicationQuit()
    {
        DOTween.KillAll();
    }
}
