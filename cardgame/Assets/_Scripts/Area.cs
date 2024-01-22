using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class Area : MonoBehaviour
{
    [InlineEditor]public AIPlayerBase sourceAI;
    [InlineEditor]public List<AIPlayerBase> aIsinArea;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
        if (sourceAI.aiState.isEnemy)
        {
            _spriteRenderer.color = new Color(255,0,0,0.1f);
        }
        else
        {
            _spriteRenderer.color = new Color(0,255,0,0.1f);
        }
    }

    private void OnEnable()
    {
        DOVirtual.DelayedCall(3, ()=>
        {
            for (int i = 0; i < aIsinArea.Count; i++)
            {
                sourceAI.OnGoing(aIsinArea[i]);
            }
        }
         ).SetLoops(-1,LoopType.Restart);

    }
    private void OnTriggerEnter(Collider other)
    {   
        AIPlayerBase aIinterface = other.GetComponent<AIPlayerBase>();
        if(aIinterface != null)
        {
            aIsinArea.Add(aIinterface);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        AIPlayerBase aIinterface = other.GetComponent<AIPlayerBase>();
        if( aIsinArea.Contains(aIinterface) && aIinterface !=null)
        {
            aIsinArea.Remove(aIinterface);
        }
    }
}
