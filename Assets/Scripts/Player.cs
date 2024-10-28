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

    private void OnEnable()
    {
        
        Card.OnCardClicked += HandleCardClicked; 
        TurnController.Instance.OnTurnChanged += HandleTurnChanged;
    }

    private void Update() {
        if( Input.GetKeyDown(KeyCode.R) ) {
            ChooseRoute(playerRoute);
        }
    }

    private void HandleTurnChanged(Player currentPlayer)
    {
        if (currentPlayer == this)
        {
            Debug.Log(name + " can take their turn.");
        }
        else
        {
            Debug.Log(name + " must wait.");
        }
    }

    private void HandleCardClicked(Card card)
    {
        if (TurnController.Instance.IsCurrentPlayer(this) ) {
            Debug.Log( name + " clicked on card: " + card.GetType());
                StartCoroutine(CardAction(card));
        }
            else {
                Debug.Log("It's not " + name + "'s turn.");
            }

    }

    private IEnumerator GetCardType(Card card, Route route) {
            switch(card.GetType()) {
                case GameConstants.CARD_TYPE_HEALTH: 
                    playerBoat.Repair(card.GetValue());
                    Debug.Log(playerBoat.Integrity);
                    yield break;
                case GameConstants.CARD_TYPE_MOVEMENT: 
                    // do movement
                            Debug.Log(name + playerBoat.transform.position);                     
                            yield return StartCoroutine(playerBoat.Move(route, card.GetValue()));
                        //   break;
                        
                    
                    break;
                case GameConstants.CARD_TYPE_EMPTY:
                    playerBoat.Empty(card.GetValue());
                    Debug.Log(playerBoat.Capacity);
                    yield break;
                case GameConstants.CARD_TYPE_ANCHOR:
                    // do anchor
                    yield break;
            }
            
    }
    private IEnumerator CardAction(Card card) {
                    //GetCardType(card, playerRoute);
                    Debug.Log(playerBoat.name);
                    yield return StartCoroutine(GetCardType(card, playerRoute));  
                    TurnController.Instance.SwitchTurn();
            
    }

    private void OnDisable()
    {
        Card.OnCardClicked -= HandleCardClicked; 
        TurnController.Instance.OnTurnChanged -= HandleTurnChanged;
    }
    public void ChooseBoat(Boat boat) {

    }

    public void ChooseRoute(Route route) {
        playerBoat.transform.position = route.GetPoints()[0].transform.position;
    }

    public void DrawCard() {

    }

    public void PlayCard() {
        // podemos llamar este evento onclick de la carta. 
        // detectar tipo de carta
        // llamar metodo del bote correspondiente
    }

    public void Shuffle() {

    }
}
