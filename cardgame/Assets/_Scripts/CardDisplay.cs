using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    private bool selected, toHand = false;


    public Card card;
    public Vector3 cardHandLocation;
    public Quaternion cardHandRotation;
    float Transparentfloat = 1f;
    private bool _preview = false;
    private bool _canOpenInformation=true;

    [SerializeField] public TextMeshPro manaText;
    [SerializeField] public TextMeshPro nameText;
    [SerializeField] public TextMeshPro descText;
    [SerializeField] public SpriteRenderer cardImage;
    [SerializeField] public SpriteRenderer cardBackground;
    [SerializeField] public SpriteRenderer cardCirclePlaceholder;
    [SerializeField] public SpriteRenderer cardUI;

    private GameObject previewObject;
    public Hand CurHand;


    private Sequence To2d3dSequence,HoldSequence;

    [Header("Events")]
    public GameEvent onClick;

    public void MoveToLocation()
    {
        transform.DOLocalMove(cardHandLocation, 0.5f);
        transform.DORotateQuaternion(cardHandRotation, 0.5f);
    }
    public void SetDisplay()
    {
        manaText.text = card.manaCost.ToString();
        nameText.text = card.cardName;
        descText.text = card.desc;
        cardImage.sprite = card.artwork;
    }
    public void OnPointerClick(PointerEventData eventData)
    {

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOScale(0.09f, 0.1f).SetEase(Ease.InOutBack);
        HoldSequence.Kill();
        HoldCountdown(1, 0.1f);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOScale(0.1f, 0.1f).SetEase(Ease.InOutBack);
        if (_canOpenInformation == true)
        {
            onClick.Raise(this, null);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (selected || toHand) return;
        ToHover();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (selected || toHand) return;
        ToTheHand();
    }
    public void OnDrag(PointerEventData eventData)
    {
        selected = true;
        Vector3 mousePositionScreen = Input.mousePosition;
        mousePositionScreen.z = Mathf.Abs(Camera.allCameras[1].transform.position.z - transform.position.z);
        Vector3 mousePositionWorld = Camera.allCameras[1].ScreenToWorldPoint(mousePositionScreen);
        transform.position = mousePositionWorld;
        if (_preview)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("Floor"));
            if (hit.collider == null) return;
            previewObject.transform.position = hit.point;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        selected = false;
        ToTheHand();
        if (!_preview) return;
        DOTween.Kill(transform);
        AIStateManager stateManager = previewObject.GetComponent<AIStateManager>();
        stateManager.SwitchState(stateManager.idleState);
        CurHand.ThrowCard(this.gameObject);
    }

    private void DefineCardToAI(AIStateManager ai)
    {
        ai.currentCard = card;
    }
    private void ToHover()
    {
        transform.DOLocalMoveY(cardHandLocation.y + 0.15f, 0.1f);
        transform.DORotateQuaternion(Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, 0), 0.2f);
        transform.DOScale(transform.localScale+(Vector3.one/50),0.1f);
        transform.SetAsLastSibling();
    }
    private void ToTheHand()
    {
        toHand = true;
        transform.DOLocalMove(cardHandLocation, 0.25f).SetEase(Ease.InBack).OnComplete(() => toHand = false) ;
        transform.DORotateQuaternion(cardHandRotation, 0.2f);
        transform.DOScale(0.1f, 0.1f);
    }
    private void CardTransparent(float transparentValue, float time)
    {
        To2d3dSequence = DOTween.Sequence();
        To2d3dSequence.Append(
        DOTween.To(x => Transparentfloat = x, Transparentfloat, transparentValue, time).OnUpdate(() =>
        {
            cardUI.material.SetFloat("_Transparent", Transparentfloat);
            cardImage.material.SetFloat("_Transparent", Transparentfloat);
            cardCirclePlaceholder.material.SetFloat("_Transparent", Transparentfloat);
            cardBackground.material.SetFloat("_Transparent", Transparentfloat);
            nameText.color = new Color(nameText.color.r, nameText.color.g, nameText.color.b, transparentValue * 255);
            descText.color = new Color(descText.color.r, descText.color.g, descText.color.b, transparentValue * 255);
            manaText.color = new Color(manaText.color.r, manaText.color.g, manaText.color.b, transparentValue * 255);
        }));
    }
    private void HoldCountdown(float notMatter, float timer)
    {
        HoldSequence = DOTween.Sequence();
        _canOpenInformation = true;
        HoldSequence.Append(
        DOTween.To(x => notMatter = x, notMatter, notMatter, timer).OnComplete(() =>
        {
            _canOpenInformation = false;
        }));
    }
    public void PreviewMode()
    {
        _preview = true;
        SpawnWorld();
        To2d3dSequence.Kill();
        CardTransparent(0, 0.15f);
        transform.DOScale(0.04f, .15f);
    }
    public void DefaultMode()
    {
        _preview = false;
        DeSpawnWorld();
        To2d3dSequence.Kill();
        transform.DOScale(0.1f, .3f);
        CardTransparent(1, 0.1f);
    }
    private void DeSpawnWorld()
    {
        if (!previewObject) return;
        previewObject.SetActive(false);
    }
    private void SpawnWorld()
    {
        if (previewObject)
        {
            previewObject.SetActive(true);
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        if (hit.collider == null) return;
        GameObject gameObject = Instantiate(card.previewPrefab, hit.transform.position, Quaternion.identity);
        previewObject = gameObject;
        DefineCardToAI(previewObject.GetComponent<AIStateManager>());
    }

    private void OnDisable()
    { 
        DOTween.Kill(transform);
        To2d3dSequence.Kill();
        HoldSequence.Kill();
    }
}