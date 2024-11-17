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
    public void SetCardAppearance(Card card) {
        valueText.text = card.firstValue > 0 ? card.firstValue.ToString() + card.action.ToString() : "" + card.action.ToString();
        cardImage.sprite = card.image;
    }
}
