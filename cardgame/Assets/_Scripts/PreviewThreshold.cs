using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewThreshold : MonoBehaviour
{
     void OnTriggerEnter2D(Collider2D collision)
    {

        CardDisplay _cardDisplay = collision.gameObject.GetComponent<CardDisplay>();
        if (_cardDisplay)
        {
            _cardDisplay.DefaultMode();
            Debug.Log(_cardDisplay.card.name + "Girdi");
        }
    }
     void OnTriggerExit2D(Collider2D collision)
    {
        CardDisplay _cardDisplay = collision.gameObject.GetComponent<CardDisplay>();
        if (_cardDisplay)
        {
            _cardDisplay.PreviewMode();
            Debug.Log(_cardDisplay.card.name + "Çýktý");
        }
    }
}
