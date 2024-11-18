using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Point : MonoBehaviour
{

    [SerializeField] private PointType type;
    [SerializeField] private int value = 1;

    [SerializeField] GameObject tooltipUI;
    [SerializeField] TMP_Text tooltipText;

    private void Start() {
        tooltipUI.SetActive(false);
    }

    // Acá lo que hacemos es cambiar el color del punto si el mouse está posado sobre ellos, para indicar que es seleccionable. 
    private void OnMouseOver() {
        if(value == 0) { return;}
        gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        tooltipUI.SetActive(true);
        tooltipText.text = value.ToString();

        tooltipUI.transform.position = transform.position + Vector3.up;
    }

    private void OnMouseExit() {
        if(value == 0) { return;}
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        tooltipUI.SetActive(false);
    }

    public Vector2 GetPosition() {
        return transform.position;
    }

    public void ChangeColor() {
        gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    public void ChangeColorBack() {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public new PointType GetType() {
        return type;
    }

    public int GetValue() {
        return value;
    }

}
