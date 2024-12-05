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

                action = crewCardData.action;  
                firstValue = crewCardData.firstValue;  
            }
            
            if (hit.collider == secondActionCollider)
            {
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