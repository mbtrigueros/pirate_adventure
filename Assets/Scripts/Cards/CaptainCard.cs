using System.Collections;
using UnityEngine;
public class CaptainCard: Card {

    public CaptainCardData captainCardData;
    public int secondValue; 
    public CardAction additionalAction;

    public GameObject arrowDown;

    public override void Awake() {
        secondValue = captainCardData.secondValue;
        additionalAction = captainCardData.additionalAction;
        base.Awake();
    }

    public override void OnMouseDown()
    {
        StartCoroutine(ArrowAnimation());
        base.OnMouseDown();
    }

        IEnumerator ArrowAnimation()
    {    
        arrowDown.SetActive(true);
        yield return new WaitForSeconds(.3f);
        arrowDown.SetActive(false);
    }

}