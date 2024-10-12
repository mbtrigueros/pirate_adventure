using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    private void OnMouseOver() {
        gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    private void OnMouseExit() {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
