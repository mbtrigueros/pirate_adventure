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
        arrowDown.SetActive(true);
        StartCoroutine(DeactivateArrow());
        base.OnMouseDown();
    }

        IEnumerator DeactivateArrow()
    {
        arrowDown.SetActive(false);
        yield return new WaitForSeconds(.5f);
    }

}