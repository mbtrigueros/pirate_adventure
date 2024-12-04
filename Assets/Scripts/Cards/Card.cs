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

    public List<Card> cardList;


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

    private void Start() {
        
        cardList.Add(this);
        Debug.Log("Added " + this + " to " + cardList);
    }

    bool clicked = false;

    public virtual void OnMouseOver() {
        if (clicked) { return; }
        Debug.Log("I'm hovering the card");
        gameObject.GetComponent<Animator>().Play("scale_in");
        //gameObject.GetComponent<Animator>().Play("change_color_gray");
    }    

    public virtual void OnMouseExit() {
        if (clicked) { return; }
        Debug.Log("I'm exiting the card");
       // gameObject.GetComponent<Animator>().Play("change_color_gray_back");
        gameObject.GetComponent<Animator>().Play("scale_out");
    }    

    public virtual void OnMouseDown() {

        if(cardData.type != CardType.CREW) gameObject.GetComponent<Animator>().Play("change_color");
        StartCoroutine(WaitForRotateToEnd());
    }

    public virtual IEnumerator WaitForRotateToEnd()
    {
        AnimatorStateInfo stateInfo =gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        
        // Wait until the animation finishes
        while (stateInfo.normalizedTime < 1f)
        {
            clicked = true;
            stateInfo = gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            yield return null;
        }

        OnCardClicked?.Invoke(this);
        clicked = false;
    }
    
}

