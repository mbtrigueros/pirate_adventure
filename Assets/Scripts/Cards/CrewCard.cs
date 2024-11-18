using UnityEngine;

public class CrewCard : Card
{
    // Data for the crew card and the colliders
    public CrewCardData crewCardData;
    public int secondValue;
    public CardAction alternativeAction;

    // References to the colliders for the left and right actions
    public Collider2D firstActionCollider;
    public Collider2D secondActionCollider;

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
        // Get the mouse position in world space (Z = 0 for 2D)
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;  // Set Z to 0 since it's a 2D game

        // Perform a raycast at the mouse position
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);  // Raycast in the direction of the mouse position

        // Debugging: log the mouse position and the hit collider
        Debug.Log($"Mouse Position: {mousePosition}");

        // If a collider is hit, check which one it is
        if (hit.collider != null)
        {
            // Check if the hit collider is the first action's collider
            if (hit.collider == firstActionCollider)
            {
                Debug.Log("First action clicked, performing action with value: " + firstValue);
                // Perform the first action
                action = crewCardData.action;  // Example: Assign the card's action
                firstValue = crewCardData.firstValue;  // Set the value for the first action
            }
            // Check if the hit collider is the second action's collider
            else if (hit.collider == secondActionCollider)
            {
                // Perform the alternative action
                action = alternativeAction;
                firstValue = secondValue;  // Use secondValue as the firstValue for the action
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

        // Always call base to ensure any additional functionality from the base class is invoked
        base.OnMouseDown();
    }
}