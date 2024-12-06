using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private List<Player> players;
    [SerializeField] private Boat playerBoat;
    [SerializeField] private Route playerRoute;
    [SerializeField] private int cardsCount = 2;

    private List<Card> drawnCards = new List<Card>();
    public bool suscribed = false;

    private void Awake()
    {
        if (TurnController.Instance == null)
        {
            Debug.LogWarning("TurnController not ready, retrying...");
            return;
        }

        if (TurnController.Instance != null)
        {
            TurnController.Instance.OnTurnChanged += HandleTurnChanged;
            Card.OnCardClicked += PlayCard;
            suscribed = true;
            Debug.Log(this + " Suscribed to the events.");
        }
        else
        {
            Debug.LogWarning("TurnController instance is null, cannot subscribe.");
        }
    }

    private void Start() {
        players.Add(this);
        if (playerBoat != null && playerRoute != null) {
            playerBoat.OnBoatSunk += HandleBoatSunk;
            playerBoat.OnBoatWin += HandleBoatWin;
            playerBoat.ResetToPort(playerRoute);
        } else {
            Debug.LogError("No boat found in the scene.");
        }
    }

    public void HandleBoatWin(Player currentPlayer)
    {
        Debug.Log(this.name + "won!!!!!!!!");
        AudioManager.Instance.PlaySound("Victoria");
      //  Restart();
    }

    private void HandleBoatSunk()
    {
        Debug.Log("The boat has sunk.");
        playerBoat.ResetToPort(playerRoute);
    }

    private void OnDisable()
    {
        Debug.Log(this + " Unsuscribed from the events.");
        Card.OnCardClicked -= PlayCard;
        TurnController.Instance.OnTurnChanged -= HandleTurnChanged;
        suscribed = false;
        playerBoat.OnBoatSunk -= HandleBoatSunk;
    }

    private void Update()
    {
        if( Input.GetKeyDown(KeyCode.R)) {
        //    Restart();
            Debug.Log("Restart game");
        } 
    }

    public void HandleTurnChanged(Player currentPlayer)
    {
        Debug.Log($"{name} received turn change. Current player is {currentPlayer.name}");
        if (currentPlayer == this)
        {
            Debug.Log(name + " can take their turn.");
            playerBoat.GetComponent<Animator>().Rebind();
            playerBoat.GetComponent<Animator>().Play("blink_animation_boat");
            DrawCards();
        }

        else {
            playerBoat.GetComponent<Animator>().Rebind();
            playerBoat.GetComponent<Animator>().Play("blink_animation_idle");
        }
    }

    private void PlayCard(Card card)
    {
        Debug.Log($"Attempting to play card: {card.type}. Current player: {TurnController.Instance.CurrentPlayer().name}. This player: {name}");
        if (TurnController.Instance.IsCurrentPlayer(this))
        {
            Debug.Log("Current player confirmed.");
            Debug.Log(name + " used the card: " + card.type);
            StartCoroutine(CardPlayAction(card));
        }
        else
        {
            Debug.Log("It's not the turn of " + name);
        }
    }

    private IEnumerator CardPlayAction(Card card)
    {
        yield return StartCoroutine(GetCardAction(card, playerRoute));
        playerBoat.GetBoatDeck().DiscardAll();     
        yield return StartCoroutine(TurnController.Instance.SwitchTurnCoroutine());
    }

    private IEnumerator GetCardAction(Card card, Route route)
    {

        CaptainCard captainCard = card as CaptainCard;
        switch (card.action)
        {
            case CardAction.HEALTH:
            if (captainCard ) { 
                playerBoat.TakeWater(captainCard.secondValue); 
                AudioManager.Instance.PlaySound("Agua");    
            }
                AudioManager.Instance.PlaySound("Reparar");
                playerBoat.Repair(card.firstValue);
                yield break;
            case CardAction.MOVEMENT:
            if (captainCard) { 
                    playerBoat.TakeWater(captainCard.secondValue); 
                    AudioManager.Instance.PlaySound("Agua");  
                }
                AudioManager.Instance.PlaySound("Movimiento");
                playerBoat.Move(route, card.firstValue);
                yield break;
            case CardAction.EMPTY:
                AudioManager.Instance.PlaySound("Vaciar");
                playerBoat.Empty(card.firstValue);
                yield break;
            case CardAction.ATTACK:
                Debug.Log("Attepmting to use ATTACK card...");
                AudioManager.Instance.PlaySound("Ataque");
                foreach(Player player in players) {
                    if (player != this) {
                        Debug.Log($"{name} is attacking {player.name}'s boat with {card.firstValue} damage.");
                        player.playerBoat.TakeDamage(card.firstValue);
                    }
                }
                yield break;
            case CardAction.BUOY:
            AudioManager.Instance.PlaySound("Boya");
                playerBoat.Buoy(route);
                yield break;
        }
    }

    public void DrawCards() {
        Cards deck = playerBoat.GetBoatDeck();
        if (deck)   {
            drawnCards.Clear();
            List<Card> newCards = deck.DrawCards(cardsCount);
            drawnCards.AddRange(newCards);
            Debug.Log(name + " has drawn " + newCards.Count + " cards.");
        } else {
            Debug.LogError("No deck found for the player boat!");
        }
    }

    public void ClearDrawnCards()
{
    Debug.Log("Before clearing: " + drawnCards.Count);
    foreach (Card card in drawnCards)
    {
        card.gameObject.SetActive(false);
        Debug.Log("Clearing the drawn cards...");
    }
    drawnCards.Clear();
    Debug.Log("After clearing: " + drawnCards.Count);
}

    public Boat GetPlayerBoat() {
        return playerBoat;
    }

    public Route GetPlayerRoute() {
        return playerRoute;
    }

}
