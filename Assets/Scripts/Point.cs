using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{

    // Acá lo que hacemos es cambiar el color del punto si el mouse está posado sobre ellos, para indicar que es seleccionable. 
    private void OnMouseOver() {
        gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    private void OnMouseExit() {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
