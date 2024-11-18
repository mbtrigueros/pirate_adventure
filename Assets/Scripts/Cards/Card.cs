using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

// En esta clase vamos a implementar todo lo que refiere al comportamiento de la Carta.
public class Card : MonoBehaviour
{
    public CardActionData cardData;
    public CardType type;
    public CardAction action;

    public Sprite image;
    public string name;
    public int firstValue;


    public delegate void CardClickedHandler(Card card);
    public static event CardClickedHandler OnCardClicked;

    public virtual void Awake()
    {
        type = cardData.type;
        action = cardData.action;
        name = cardData.name;
        firstValue = cardData.firstValue;
        image = cardData.image;
    }

    public virtual void OnMouseDown() {
        OnCardClicked?.Invoke(this);
    }    
}
