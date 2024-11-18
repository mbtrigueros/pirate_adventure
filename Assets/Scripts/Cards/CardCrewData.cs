using UnityEngine;

[CreateAssetMenu(fileName = "CrewCardData", menuName = "Card/CrewCard")]
public class CrewCardData : CardActionData
{
    public CardAction alternativeAction;
    public int secondValue; 
}