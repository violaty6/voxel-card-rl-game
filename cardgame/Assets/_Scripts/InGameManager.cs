using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector3 = UnityEngine.Vector3;

[DefaultExecutionOrder(-1000)]
public class InGameManager : MonoBehaviour
{
    public List<AIPlayerBase> EnemiesInDungeon;
    public List<AIPlayerBase> AlliesInDungeon;
    [SerializeField]private AIStateManager currentSelectedAI;
    [SerializeField] GameObject SelectedSprite;
    public GameEvent onSelect;
    private void Awake()
    {
        ServiceLocator.Initiailze();
        DefaultInputActions dplayerActions = new DefaultInputActions();
        dplayerActions.Enable();
        dplayerActions.UI.Click.performed += SelectRay;
    }
    private void SelectRay(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100,LayerMask.GetMask("AI")))
        {
            onSelect.Raise(hit.transform.GetComponentInParent<AIStateManager>(),null);
        }
    }
    public void SelectAI(Component sender, object data)
    {
        AIStateManager ai = sender as AIStateManager;
        if (ai != null)
        {
            SelectedSpriteFeedBack(SelectedSprite);
            SelectedSprite.transform.position = ai.transform.position + Vector3.up/10;
            SelectedSprite.transform.parent = ai.transform;
            currentSelectedAI = ai;
        }
        else
        {
            Debug.LogWarning("The sender is not of type Card.");
        }

    }
    public void DeSelectAI(Component sender, object data)
    {
        AIStateManager ai = sender as AIStateManager;
        Debug.Log(ai);
        Debug.Log(currentSelectedAI);
        if (ai == currentSelectedAI)
        {
            Debug.Log("Raised");
            SelectedSprite.transform.position = new Vector3(100,100,100);
            SelectedSprite.transform.parent = null;
            currentSelectedAI = null;
        }
        else
        {
            Debug.LogWarning("The sender is not of type Card.");
        }

    }
    public void CardInit(Component sender, object[] data)
    {
        AIPlayerBase ai = sender as AIPlayerBase;
        if (ai != null)
        {
            if((bool) data[0])
            {
                EnemiesInDungeon.Add(ai);
            }
            else
            {
                AlliesInDungeon.Add(ai);
            }
        }
        else
        {
            Debug.LogWarning("The sender is not of type Card.");
        }

    }
    private void SelectedSpriteFeedBack(GameObject sprite)
    {
        sprite.transform.DOLocalRotate(new Vector3(0,360,0), 13f).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear).SetRelative();
    }
}
