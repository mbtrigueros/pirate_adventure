using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    [SerializeField] Card card;

    [SerializeField] TextMeshProUGUI typeText, valueText;

    [SerializeField] Image cardImage;

    void Start()
    {
        typeText.text = card.type;
        valueText.text = "+" + card.value.ToString();
        cardImage.color = card.color;
    }
}
