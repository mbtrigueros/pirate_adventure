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

    private BoatType deckType;
    

    public void GenerateDeck(BoatType boatType) {
        deck.Clear();

        DeckData deckData = null;

        switch (boatType) {
            case BoatType.BIG:
                deckData = bigDeck;
                deckType = BoatType.BIG;
                break;
            case BoatType.FAST:
                deckData = fastDeck;
                deckType = BoatType.FAST;
                break;
            case BoatType.STRONG:
                deckData = strongDeck;     
                deckType = BoatType.STRONG;      
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

    public List<Card> DrawCards(int count) {
        if (deck.Count == 0) {
            ReshuffleDiscardDeck();
        }
        
        currentDrawnCards.Clear();



        for (int i = 0; i < count && deck.Count > 0; i++) {
            int randomIndex = Random.Range(0, deck.Count);
            Card drawnCard = deck[0];
            drawnCard.gameObject.SetActive(true);
            deck.RemoveAt(0);
            currentDrawnCards.Add(drawnCard);
            CardDisplay displayCard = drawnCard.GetComponent<CardDisplay>();
            displayCard.SetCardAppearance(drawnCard);
            DisplayCard(drawnCard, i);
            Debug.Log("i displayed the card" + displayCard);
        }

        Debug.Log("Current Deck Count after drawing cards: " + deck.Count);
        return currentDrawnCards; 

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
            CardDisplay displayCard = card.GetComponent<CardDisplay>();
            card.type = cardType;
            // displayCard.SetAlpha(card, 0f);
           // card.gameObject.SetActive(false);
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
            card.gameObject.SetActive(false);
            DiscardCard(card);
        }

        currentDrawnCards.Clear();
    }

    public List<Card> GetCurrentDrawnCards() {
        return currentDrawnCards;
    }

    public BoatType GetDeck() {
        return deckType;
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
