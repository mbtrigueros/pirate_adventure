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

    private Color originalColor;
    
    public void Start() {
        originalColor = cardImage.color;
    }

    public void Update() {

    }
    public void DisableCard() {
        gameObject.SetActive(false);
    }
    public void SetCardAppearance(Card card) {
        cardImage.color = originalColor;

        if (card) {
        valueText.text = card.firstValue > 0 ? card.firstValue.ToString() : "";

        if (secondValueText) {
            if( card is CrewCard crewCard) {
                secondValueText.text = crewCard.secondValue.ToString();
            }
            if (card is CaptainCard captainCard) {
                secondValueText.text = captainCard.secondValue.ToString();
            }

        }
        cardImage.sprite = card.image;
        }

    }
}
