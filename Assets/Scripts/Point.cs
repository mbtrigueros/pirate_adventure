using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{

    [SerializeField] private PointType type;
    [SerializeField] private int value = 1;

    // Acá lo que hacemos es cambiar el color del punto si el mouse está posado sobre ellos, para indicar que es seleccionable. 
    private void OnMouseOver() {
        gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    private void OnMouseExit() {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public Vector2 GetPosition() {
        return transform.position;
    }

    public PointType GetType() {
        return type;
    }

    public int GetValue() {
        return value;
    }

}
