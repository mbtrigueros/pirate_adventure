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

    [SerializeField] Sprite buoySprite;

    private Color spriteOriginalColor;

    private Sprite originalSprite;

    private void Start() {
        tooltipUI.SetActive(false);
        spriteOriginalColor = gameObject.GetComponent<SpriteRenderer>().color;
        originalSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
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
        gameObject.GetComponent<SpriteRenderer>().color = spriteOriginalColor;
        tooltipUI.SetActive(false);
    }

    public Vector2 GetPosition() {
        return transform.position;
    }

    public void ChangeColor() {
        gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    public void ThrowBuoy() {
        gameObject.GetComponent<SpriteRenderer>().sprite = buoySprite;
    }

    public void ResetSprite() {
        gameObject.GetComponent<SpriteRenderer>().sprite = originalSprite;
    }


    public void ChangeColorBack() {        
        gameObject.GetComponent<SpriteRenderer>().color = spriteOriginalColor;
    }

    public new PointType GetType() {
        return type;
    }

    public int GetValue() {
        return value;
    }

}
