using UnityEngine;

[CreateAssetMenu(fileName = "CaptainCardData", menuName = "Card/CaptainCard")]
public class CaptainCardData : CardActionData
{    
    public CardAction additionalAction = CardAction.EMPTY;
    public int secondValue; 
}