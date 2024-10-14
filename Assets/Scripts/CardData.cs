using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Esta clase es diferente a las demás que hemos trabajado hasta ahora. Esta clase hereda de ScriptableObject, algo que nos ofrece Unity para poder armar una especie de base de datos editable facilmente. 

// Esta linea nos permite crear una nueva opción en el menu de Unity para crear un nuevo Scriptable Object "Carta". 
[CreateAssetMenu(fileName ="New Card", menuName ="Card")]
public class CardData : ScriptableObject
{
    // Estos campos son la información o stats de la carta en cuestión. Vamos a asignarles a cada una un color, un tipo (ej: de movimiento) y un valor (ej: + 1).
    // Al ser esto un scriptable object podemos armar diferentes tipos de cartas y modificarlas facilmente desde el editor.
    public Color color;
    public string type;
    public int value;
}
