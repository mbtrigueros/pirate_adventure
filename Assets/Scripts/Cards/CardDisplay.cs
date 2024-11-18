using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    [SerializeField] public Image cardImage;
    [SerializeField] public TextMeshProUGUI valueText;
    [SerializeField] public TextMeshProUGUI secondValueText;
    
    public void Start() {
    }

    public void Update() {

    }
    public void SetCardAppearance(Card card) {
        if (card) {
        valueText.text = card.firstValue > 0 ? card.firstValue.ToString() : "";
        if (secondValueText && card is CrewCard crewCard) {
            secondValueText.text = crewCard.secondValue.ToString();
        }
        cardImage.sprite = card.image;

            switch(card.action) {
            case CardAction.HEALTH: 
                cardImage.color = Color.green;
                break;
            case CardAction.MOVEMENT:
                cardImage.color = Color.yellow;
                break;
            case CardAction.ATTACK:
                cardImage.color = Color.red;
                break;
            case CardAction.EMPTY: 
                cardImage.color = Color.blue;
                break;
            case CardAction.BUOY:
                cardImage.color = Color.magenta;
                break;
        }
        }

    }
}
