using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    [SerializeField] public Image cardImage {get; private set;}
    [SerializeField] public TextMeshProUGUI valueText {get; private set;}
    

    public void SetCardAppearance(Card card)
    {
        
        valueText.text = card.value > 0 ? card.value.ToString() : "Anchor";

        switch(card.type) {
            case CardType.HEALTH: 
                cardImage.color = Color.green;
                break;
            case CardType.MOVEMENT:
                cardImage.color = Color.yellow;
                break;
            case CardType.EMPTY: 
                cardImage.color = Color.blue;
                break;
            case CardType.ANCHOR:
                cardImage.color = Color.red;
                break;
        }
    }
}
