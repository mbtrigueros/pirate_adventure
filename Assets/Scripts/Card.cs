using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// En esta clase vamos a implementar todo lo que refiere al comportamiento de la Carta.
public class Card : MonoBehaviour
{
    [SerializeField] CardData card;

    [SerializeField] TextMeshProUGUI typeText, valueText;

    [SerializeField] Image cardImage;

    void Start()
    {
        typeText.text = card.type;
        valueText.text = "+" + card.value.ToString();
        cardImage.color = card.color;
    }
}
