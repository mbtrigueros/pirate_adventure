using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// La clase Cards plural se encargará del comportamiento del mazo de cartas, por el momento único, pero luego serán de acuerdo a cada tipo de barco. 
public class Cards : MonoBehaviour
{

    [SerializeField] GameObject cardPrefab;
    [SerializeField] List<Transform> cardPositions;
    [SerializeField] private DeckData strongDeck, bigDeck, fastDeck; 
    private List<Card> deck = new List<Card>();
    private List<Card> discardDeck = new List<Card>();
    private List<Card> currentDrawnCards = new List<Card>(); 
    

    public void GenerateDeck(BoatType boatType) {
        deck.Clear();

        DeckData deckData = null;

        switch (boatType) {
            case BoatType.BIG:
                deckData = bigDeck;
                break;
            case BoatType.FAST:
                deckData = fastDeck;
                break;
            case BoatType.STRONG:
                deckData = strongDeck;           
                break;
        }

        if (deckData) {
            AddCardsToDeck(CardType.HEALTH, deckData.healthCardCount);
            AddCardsToDeck(CardType.MOVEMENT, deckData.movementCardCount);
            AddCardsToDeck(CardType.EMPTY, deckData.emptyCardCount);
            AddCardsToDeck(CardType.ANCHOR, deckData.anchorCardCount);
        }

        Shuffle();
    }

    public void DrawCards(int count) {
        if (deck.Count == 0) {
            ReshuffleDiscardDeck();
        }
        
        currentDrawnCards.Clear();



        for (int i = 0; i < count && deck.Count > 0; i++) {
            int randomIndex = Random.Range(0, deck.Count);
            Card drawnCard = deck[0];
            deck.RemoveAt(0);
            CardDisplay displayCard = drawnCard.GetComponent<CardDisplay>();
            displayCard.SetCardAppearance(drawnCard);
            DisplayCard(drawnCard, i);
            Debug.Log("i displayed the card" + displayCard.cardImage.color + displayCard.valueText);
        }

    }

    private void ReshuffleDiscardDeck()
    {
        if (discardDeck.Count > 0) {
            deck.AddRange(discardDeck);
            discardDeck.Clear();
            Shuffle();
        }
    }

    private void AddCardsToDeck(CardType cardType, int count) {
        for(int i = 0; i < count; i++) {
            GameObject cardObject = Instantiate(cardPrefab);
            Card card = cardObject.GetComponent<Card>();
            card.type = cardType;
            if ( cardType != CardType.ANCHOR ) {
                card.value = Random.Range(1, 4);
            }
            else {
                card.value = 0;
            }
            deck.Add(card);
        }
    }

    private void DisplayCard(Card card, int positionIndex)
    {
        if (positionIndex < cardPositions.Count)
        {
            card.transform.position = cardPositions[positionIndex].position;
            card.gameObject.SetActive(true); 
        }
    }

    public void DiscardCard(Card card) {
        discardDeck.Add(card);
    }

    public void DiscardAll() {
        foreach(Card card in currentDrawnCards) {
            DiscardCard(card);
        }

        currentDrawnCards.Clear();
    }

    public List<Card> GetDeck() {
        return deck;
    }

    public void Shuffle() {
        for (int i = 0; i < deck.Count; i++) {
            Card indexCard = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = indexCard;
        }
    }

}
