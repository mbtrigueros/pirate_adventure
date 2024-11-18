using UnityEngine;
public class CaptainCard: Card {

    public CaptainCardData captainCardData;
    public int secondValue; 
    public CardAction additionalAction;

    public override void Awake() {
        secondValue = captainCardData.secondValue;
        additionalAction = captainCardData.additionalAction;
        base.Awake();
    }
}