using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// En esta clase vamos a implementar todo lo que refiere al comportamiento de la Carta.
public class Card : MonoBehaviour
{
    public CardType type;
    public CardAction action;
    public int value;


    public delegate void CardClickedHandler(Card card);
    public static event CardClickedHandler OnCardClicked;

    void Start()
    {

    }

    public void OnMouseDown() {
        OnCardClicked?.Invoke(this);
    }    
}
