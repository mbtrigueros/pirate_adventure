using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// La clase Cards plural se encargará del comportamiento del mazo de cartas, por el momento único, pero luego serán de acuerdo a cada tipo de barco. 
public class Cards : MonoBehaviour
{
    [SerializeField] List<Transform> cardPositions;
    [SerializeField] private List<Card> deck = new List<Card>();
    [SerializeField] Transform deckGameObjectParent;
    private List<Card> discardDeck = new List<Card>();
    private List<Card> currentDrawnCards = new List<Card>(); 
    

    public void GenerateDeck() {
        if (deck == null || deck.Count == 0) {
        Debug.LogError("Deck is empty or not initialized!");
        return;  // Exit the method early if the deck is empty or null
    }
        discardDeck.Clear(); 

        foreach(Card card in deck) {
            card.gameObject.SetActive(false);
        }

        Shuffle();
    }


    public void RestartDeck() {
        DiscardAll();
        ReshuffleDiscardDeck();
        Debug.Log("Deck has been restarted.");
    }
    
    public List<Card> DrawCards(int count) {
        if (deck.Count == 0) {
            Debug.Log("Deck count before reshuffling: " + deck.Count);
            Debug.Log("Discard count before reshuffling: " + discardDeck.Count);
            ReshuffleDiscardDeck();
        }
        
        currentDrawnCards.Clear();


        for (int i = 0; i < count && deck.Count > 0; i++) {
            int randomIndex = Random.Range(0, deck.Count);
            Card drawnCard = deck[0];
            DisplayCard(drawnCard, i);
            Debug.Log("AFTER CARD POSITION " + drawnCard.transform.position);
            drawnCard.gameObject.SetActive(true);
            deck.RemoveAt(0);
            currentDrawnCards.Add(drawnCard);
            CardDisplay displayCard = drawnCard.GetComponent<CardDisplay>();
            displayCard.SetCardAppearance(drawnCard);
            AudioManager.Instance.PlaySound("Carta");
        }

        Debug.Log("Current Deck Count after drawing cards: " + deck.Count);
        return currentDrawnCards; 

    }

    private void DisplayCard(Card card, int positionIndex)
    {
        if (positionIndex < cardPositions.Count)
        {
            card.transform.position = cardPositions[positionIndex].transform.position;
        }
    }

    public int GetDeckCount()
    {
        return deck.Count;
    }

    public void ReshuffleDiscardDeck()
    {
        if (discardDeck.Count > 0) {
            deck.AddRange(discardDeck);
            discardDeck.Clear();
            Shuffle(); 
            Debug.Log("Deck reshuffled. Cards remaining: " + deck.Count);
        }
        else {
            Debug.Log("No cards to reshuffle.");
        }
    }

    public void DiscardCard(Card card) {
        // if (card.action == CardAction.BUOY) { 
        //     Debug.Log("Card was Buoy type, so once it appears it never does again.");
        //     return; }

        discardDeck.Add(card);
    }

    public void DiscardAll() {
        foreach(Card card in currentDrawnCards) {
        //     if(card.GetComponent<Animator>() != null) {
        //     card.GetComponent<Animator>().Play("fade_out");
        // }
            //card.gameObject.SetActive(false);
            DiscardCard(card);
        }

        currentDrawnCards.Clear();
        Debug.Log("All drawn cards have been discarded.");
    }

        public List<Card> GetDeck() {
        return deck;
    }

    public List<Card> GetCurrentDrawnCards() {
        return currentDrawnCards;
    }

    public void Shuffle() {
        for (int i = 0; i < deck.Count; i++) {
            Card indexCard = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = indexCard;
        }

        Debug.Log("Deck shuffled.");
    }

}
