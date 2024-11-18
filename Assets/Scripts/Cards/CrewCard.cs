using UnityEngine;
public class CrewCard: Card {

    public CrewCardData crewCardData;

    public int secondValue; 
    public CardAction alternativeAction;
    public Collider2D firstActionCollider;
    public Collider2D secondActionCollider;

    public override void Awake() {
        secondValue = crewCardData.secondValue;
        alternativeAction = crewCardData.alternativeAction;
        base.Awake();
    }

    public override void OnMouseDown()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure it's on the same plane as the card

        // Check which collider the mouse is over
        if (firstActionCollider.OverlapPoint(mousePosition))
        {
            Debug.Log("Clicked first action, performing: " + action + "with a value of: " + firstValue);
        }
        else if (secondActionCollider.OverlapPoint(mousePosition))
        {
            action = alternativeAction;
            firstValue = secondValue;
            Debug.Log("Clicked second action, performing: " + action + "with a value of: " + firstValue);
        }
        
        // Call the base class's OnMouseDown to invoke the general click event
        base.OnMouseDown();
    }
}