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

    private void OnEnable()
    {
        Card.OnCardClicked += PlayCard; 
        TurnController.Instance.OnTurnChanged += HandleTurnChanged;
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
        if (currentPlayer == this)
        {
            Debug.Log(name + " puede tomar su turno.");
        }
        else
        {
            Debug.Log(name + " debe esperar.");
        }
    }

    private void PlayCard(Card card)
    {
        if (TurnController.Instance.IsCurrentPlayer(this) ) {
            Debug.Log( name + " usó la carta: " + card.GetType());
                StartCoroutine(CardAction(card));
        }
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
                    // do movement
                            Debug.Log(name + playerBoat.transform.position);                     
                            yield return StartCoroutine(playerBoat.Move(route, card.value));
                        
                    
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

    private void OnDisable()
    {
        Card.OnCardClicked -= PlayCard; 
        TurnController.Instance.OnTurnChanged -= HandleTurnChanged;
    }
    public void ChooseBoat(Boat boat) {

    }

    public void ChooseRoute(Route route) {
        playerBoat.transform.position = route.GetPoints()[0].transform.position;
    }

    public void DrawCards() {
        Cards deck = playerBoat.GetBoatDeck();
        if(deck) deck.DrawCards(cardsCount);
    }

    public void Shuffle() {

    }
}
