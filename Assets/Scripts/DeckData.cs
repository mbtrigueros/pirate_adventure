using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New DeckData", menuName ="DeckData")]
public class DeckData : ScriptableObject
{
    public int healthCardCount;
    public int movementCardCount;
    public int emptyCardCount;
    public int anchorCardCount;
}
