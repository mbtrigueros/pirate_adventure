using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// La clase Cards plural se encargará del comportamiento del mazo de cartas, por el momento único, pero luego serán de acuerdo a cada tipo de barco. 
public class Cards : MonoBehaviour
{
    // Este campo creará una lista de cartas, el mazo, a partir de las cartas a las que hagamos referencia en el editor.
    [SerializeField] List<Card> deck = new();

    // En este campo se hará un array de slots o lugares disponibles adonde se colocarán las cartas. Representarían nuestra "mano". La palabra reservada "Transform" indica que estamos referenciando el componente Transform de en este caso los gameobjects vacíos, esto es, vamos a querer acceder a su posición. 
    [SerializeField] Transform[] cardSlots;

    // Acá armamos un array de booleanas de acuerdo a la disponibilidad de nuestra mano. Cuando un lugar deja de estar disponible, pasa a ser false y no podremos poner una carta ahí.
    [SerializeField] bool[] availableCardSlots;

    // El método DrawCard se encarga del comportamiento al apretar el botón de Retirar Carta en el juego. Esto se vincula desde el editor de Unity. 
    public void DrawCard() {
        // Primero chequeamos que el mazo tenga cartas.
        if (deck.Count >= 1) {
            // Luego aleatorizamos el mazo y le asignamos una variable a esa carta aleatoria.
            Card randomCard = deck[Random.Range(0, deck.Count)];

            // Recorremos el array de lugares disponibles en la mano
            for (int i = 0; i < availableCardSlots.Length; i++) {
                // Si hay lugar disponible
                if (availableCardSlots[i]) {
                    // Activamos la carta aleatoria en el editor.
                    randomCard.gameObject.SetActive(true);
                    // Hacemos que la posición de dicha carta sea igual a alguno de los lugares disponibles en nuestra mano.
                    randomCard.gameObject.transform.position = cardSlots[i].position;
                    // El lugar tomado pasa a no estar disponible, osea falso.
                    availableCardSlots[i] = false;
                    // Quitamos la carta agarrada del mazo.
                    deck.Remove(randomCard);
                    // Salimos del loop.
                    return;
                } 
            }

        }
    }

}
