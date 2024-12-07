using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CrewCard : Card
{
    // Data for the crew card and the colliders
    public CrewCardData crewCardData;
    public int secondValue;
    public CardAction alternativeAction;

    // References to the colliders for the left and right actions
    public Collider2D firstActionCollider;
    public Collider2D secondActionCollider;

    public UnityEngine.UI.Image overlayLeft;
    public UnityEngine.UI.Image overlayRight;

    public GameObject highlightLeft, highlightRight;

    // Called when the card is created/initialized
    public override void Awake()
    {
        if (crewCardData) {
            secondValue = crewCardData.secondValue;
            alternativeAction = crewCardData.alternativeAction;
            
            image = crewCardData.image;

            // Debugging: Check if the data is correctly assigned
            Debug.Log("CrewCard Initialized");
            Debug.Log("Second Value: " + secondValue);
            Debug.Log("Alternative Action: " + alternativeAction);
        }

        base.Awake();
    }

    public override void OnMouseOver() {
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;  

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero); 

        if (hit.collider != null)
        {
            if (hit.collider == firstActionCollider)
            {

                StartCoroutine(AnimationLeft());
            }
            
            if (hit.collider == secondActionCollider)
            {
                StartCoroutine(AnimationRight());
            }
        }
        base.OnMouseOver();    
        }

    public override void OnMouseExit()
    {
        highlightLeft.SetActive(false);
        highlightRight.SetActive(false);
        base.OnMouseExit();
    }

    IEnumerator AnimationLeft()
    {
        highlightLeft.SetActive(true);
        yield return new WaitForSeconds(20f);
        highlightLeft.SetActive(false);
    }

    IEnumerator AnimationRight()
    {
        highlightRight.SetActive(true);
        yield return new WaitForSeconds(20f);
        highlightRight.SetActive(false);
        
    }

    // Called when the mouse is clicked on the card
    public override void OnMouseDown()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;  

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero); 

        if (hit.collider != null)
        {
            if (hit.collider == firstActionCollider)
            {
                Debug.Log("First action clicked, performing action with value: " + firstValue);

                StartCoroutine(AnimationLeft());

                
                action = crewCardData.action;  
                firstValue = crewCardData.firstValue;  
            }
            
            if (hit.collider == secondActionCollider)
            {
                StartCoroutine(AnimationRight());
                

                action = alternativeAction;
                firstValue = secondValue;  
                Debug.Log("Second action clicked, performing " + action + " with value: " + secondValue);
            }
            else
            {
                Debug.Log("Clicked outside the action areas.");
            }
        }
        else
        {
            Debug.Log("No collider was hit.");
        }

        base.OnMouseDown();
    }
}