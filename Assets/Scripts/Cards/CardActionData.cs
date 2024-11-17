using UnityEngine;

[CreateAssetMenu(fileName = "CardActionData", menuName = "Card/ActionCard")]
public class CardActionData : ScriptableObject {
    public new string name;
    public CardType type;
    public Sprite image;
    public CardAction action;
    public int firstValue; 
}