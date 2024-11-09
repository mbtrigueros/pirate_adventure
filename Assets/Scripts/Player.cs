using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


// Clase Player. Esta clase se encargará de los comportamientos del jugador, en un principio es un solo archivo pero probablemente abarque más dependiendo de la complejidad. 
// Podemos definir una clase como un "tipo" de objeto. 
// Para crear una clase vamos a usar la palabra reservada "class" y definiremos si es pública (public) o privada (private). 
// En Unity, la mayoría de los scripts que creamos van a heredar de la clase MonoBehaviour. Esta clase es creada por Unity, y facilita muchas cosas a la hora del manejo de un elemento de juego. Por ejemplo, contiene los métodos Start() y Update(), entre otros.
// Que significa que "hereda"? Cuando una clase hereda de otra, esto quiere decir que puede utilizar métodos, campos y variables que definen o pertenecen a dicha clase. Ejemplo: Existe una clase Perro con el método Ladrar(). Tanto un Caniche como un Ovejero Alemán, que serían en este ejmplo distintas clases, van a heredar de la clase Perro y van a poder utilizar el método ladrar. 
public class Player : MonoBehaviour
{   
    
    [SerializeField] private Boat playerBoat;
    [SerializeField] private Route playerRoute;

    [SerializeField] private int cardsCount = 4;

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
            Debug.Log(this + "Suscribed to the events.");
        }
        else
        {
            Debug.LogWarning("TurnController instance is null, cannot subscribe.");
        }
            

    }

    
    private void OnDisable()
    {
            Debug.Log(this + "Unsuscribed to the events.");
        Card.OnCardClicked -= PlayCard; 
        TurnController.Instance.OnTurnChanged -= HandleTurnChanged;
        suscribed = false;
    }
    


    private void Update() {

        // Por el momento "elegimos" la ruta presionando R. En realidad, en el editor de Unity arrastramos la Ruta que queremos a cada Player. Esto se modificará más adelante. 
        // Al elegir la ruta, nuestro bote se coloca en la posición 0 de la misma.
        if( Input.GetKeyDown(KeyCode.R) ) {
            ChooseRoute(playerRoute);
        }
    }

    private void HandleTurnChanged(Player currentPlayer)
    {
        Debug.Log($"{name} received turn change. Current player is {currentPlayer.name}");
        Debug.Log($"Current Player: {currentPlayer.name}, This Player: {name}");
        if (currentPlayer == this)
        {
            Debug.Log(name + " puede tomar su turno.");
            DrawCards();
        }
    }

    private void PlayCard(Card card)
    {
        Debug.Log($"Attempting to play card: {card.type}. Current player: {TurnController.Instance.CurrentPlayer().name}. This player: {name}");
        if (TurnController.Instance.IsCurrentPlayer(this) ) {
            Debug.Log("Current player confirmed.");
            if (playerBoat.GetBoatDeck().GetCurrentDrawnCards().Contains(card)) {
                Debug.Log( name + " usó la carta: " + card.type);
                    StartCoroutine(CardAction(card));
            } }
            else {
                Debug.Log("No es el turno de " + name);
            }
    }

    private IEnumerator GetCardType(Card card, Route route) {
            switch(card.type) {
                case CardType.HEALTH: 
                    playerBoat.Repair(card.value);
                    Debug.Log(playerBoat.Integrity);
                    yield break;
                case CardType.MOVEMENT:                   
                    playerBoat.Move(route, card.value);
                    break;
                case CardType.EMPTY:
                    playerBoat.Empty(card.value);
                    Debug.Log(playerBoat.Capacity);
                    yield break;
                case CardType.ANCHOR:
                    // do anchor
                    yield break;
            }
            
    }
    private IEnumerator CardAction(Card card) {
                    Debug.Log(playerBoat.name);
                    yield return StartCoroutine(GetCardType(card, playerRoute));  
                    playerBoat.GetBoatDeck().DiscardAll();
                    TurnController.Instance.SwitchTurn();
            
    }


    public void ChooseBoat(Boat boat) {

    }

    public void ChooseRoute(Route route) {
        playerBoat.transform.position = route.GetPoints()[0].transform.position;
    }

    public void DrawCards() {
        Cards deck = playerBoat.GetBoatDeck();
        if(deck) {
            List<Card> newCards = deck.DrawCards(cardsCount);
            drawnCards.AddRange(newCards); 
            Debug.Log(name + " has drawn " + newCards.Count + " cards.");
        }
    }

    public void ClearDrawnCards() {
        Debug.Log("Before clearing: " + drawnCards.Count);
        foreach (Card card in drawnCards) {
            card.GetComponent<CardDisplay>().SetAlpha(card, 0f); 
            Debug.Log("Im clearing the drawncards...");
        }
        drawnCards.Clear();
        Debug.Log("After clearing: " + drawnCards.Count);
    }

    public void Shuffle() {

    }
}
