using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Point : MonoBehaviour
{

    [SerializeField] private PointType type;
    [SerializeField] private int value = 1;

    [SerializeField] GameObject tooltipUI;
    [SerializeField] TMP_Text tooltipText;

    [SerializeField] GameObject buoySprite;

    private Color spriteOriginalColor;

    private Sprite originalSprite;

    private Vector3 originalScale;

    private Material originalMaterial;

    [SerializeField] Material buoyMaterial;


    [SerializeField] GameObject winMenu;

    private void Start() {
        tooltipUI.SetActive(false);
        spriteOriginalColor = gameObject.GetComponent<SpriteRenderer>().color;
        originalSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        originalScale = gameObject.transform.localScale;
        originalMaterial = gameObject.GetComponent<SpriteRenderer>().material;
    }

    // Acá lo que hacemos es cambiar el color del punto si el mouse está posado sobre ellos, para indicar que es seleccionable. 
    private void OnMouseOver() {
        if(value == 0 || winMenu.activeSelf == true ) { return;}
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
        if (buoySprite) buoySprite.SetActive(true);
    }

    public void ResetSprite() {
        if (buoySprite) buoySprite.SetActive(false);
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
