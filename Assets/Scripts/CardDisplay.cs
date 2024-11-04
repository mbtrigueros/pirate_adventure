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
    
    public void Start() {
    }

    public void Update() {

    }
    public void SetCardAppearance(Card card)
    {
        
        valueText.text = card.value > 0 ? card.value.ToString() : "Anchor";

      //  SetAlpha(card, 1f);

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

    public void SetAlpha(Card card, float alpha) {
        alpha = Mathf.Clamp01(alpha);
        Color color = cardImage.color;
        color.a = alpha; 
        cardImage.color = color;
    }
}
