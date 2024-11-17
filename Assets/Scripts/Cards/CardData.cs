using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "Card")]
public class CardData : ScriptableObject {
    public CardType type;
    public CardAction action;
    public new string name;
    public int value;
}