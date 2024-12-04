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
    public void DisableCard() {
        gameObject.SetActive(false);
        cardImage.color = Color.white;
    }
    public void SetCardAppearance(Card card) {
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
