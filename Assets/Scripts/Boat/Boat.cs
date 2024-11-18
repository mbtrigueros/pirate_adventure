using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Boat : MonoBehaviour
{
    [SerializeField] private Cards boatDeck;  // The Cards object containing the deck
    public int Integrity { get; private set; }
    public int Capacity { get; private set; }
    [SerializeField] private int maxIntegrity, maxCapacity;
    [SerializeField] private bool sunken = false;

    public event Action<int> OnIntegrityChanged;
    public event Action<int> OnCapacityChanged; 
    public event Action OnBoatSunk; 


    private void Awake() {
        
        // Initialize the boat deck
        if (boatDeck != null) {
            boatDeck.GenerateDeck();
            Debug.Log("Deck Generated");
        }
        else {
            Debug.LogError("Boat deck not assigned in inspector!");
        }
    }

    void Start()
    {
        Integrity = maxIntegrity;
        Capacity = maxCapacity;
    }

    void Update()
    {
        if (sunken) {
            OnBoatSunk?.Invoke();
        }
    }

    int oldPositionIndex = 0;

    // Method for moving the boat along the route
    public void Move(Route route, int cardValue) 
    {
        if (sunken) {
            oldPositionIndex = 0; 
            return;
        }
        var routeIndex = oldPositionIndex + cardValue;   
        oldPositionIndex = routeIndex;
        transform.position = route.GetPoints()[routeIndex].transform.position;
    }

    public bool buoyUsed = false;
    public void Buoy(Route route) {
        if(buoyUsed) { return;}
        buoyUsed = true;
        Debug.Log("Current Position: " + route.GetPoints()[oldPositionIndex].transform.position);
        route.GetPoints()[oldPositionIndex].ChangeColor();
        int length = route.GetPoints().Length - oldPositionIndex; 
        Point[] newRoute = new Point[length];
        System.Array.Copy(route.GetPoints(), oldPositionIndex, newRoute, 0, length);
        oldPositionIndex = 0; 
        route.SetPoints(newRoute); 
    }

    public void ResetBuoy(Route route, Point[] array) {
        
    }

    // Repair the boat by increasing its integrity
    public void Repair(int health) {
        Integrity = Mathf.Clamp(Integrity + health, 0, maxIntegrity);
        OnIntegrityChanged?.Invoke(Integrity);
        if (Integrity > 0 && Capacity > 0) {
            sunken = false; 
        }
    }

    // Empty water from the boat by decreasing its capacity
    public void Empty(int water) {
        Capacity = Mathf.Clamp(Capacity + water, 0, maxCapacity);
        OnCapacityChanged?.Invoke(Capacity);
        if (Integrity > 0 && Capacity > 0) {
            sunken = false; 
        }
    }

    public void ResetToPort(Route route)
    {
        Repair(maxIntegrity);
        Empty(maxCapacity);
        oldPositionIndex = 0; 
        transform.position = route.GetPoints()[0].transform.position;
        Debug.Log("The boat has been reset to the starting position.");
        Debug.Log("Stat have been restored. Integrity: " + Integrity + " Capacity: " + Capacity );
    }

    public int GetMaxIntegrity() {
        return maxIntegrity;
    }
    
    public int GetMaxCapacity() {
        return maxCapacity;
    }

    // Handle damage taken by the boat
    public void TakeDamage(int damage) 
    {
        Integrity = Mathf.Clamp(Integrity - damage, 0, maxIntegrity);
        Debug.Log("You took " + damage + " damage. Your integrity is now: " + Integrity);
        OnIntegrityChanged?.Invoke(Integrity);
        if (Integrity <= 0) 
        { 
            sunken = true; 
            Debug.Log("The boat has sunk.");
        }
    }

    // Handle water taken by the boat
    public void TakeWater(int water) 
    {
        Capacity = Mathf.Clamp(Capacity - water, 0, maxCapacity);      
        Debug.Log("You took on " + water + " units of water. Your capacity is now: " + Capacity);
        OnCapacityChanged?.Invoke(Capacity);
        if (Capacity <= 0) 
        { 
            sunken = true; 
            Debug.Log("The boat has sunk.");
        }
    }

    // Generate the boat's deck based on boat type
    public void GenerateBoatDeck() 
    {
        if (boatDeck != null) {
            boatDeck.GenerateDeck();
            Debug.Log("Generating deck...");
        }
        else {
            Debug.LogError("Boat deck not assigned!");
        }
    }

    public Cards GetBoatDeck() 
    {
        return boatDeck;  
    }

    private bool hasCollided = false;

    // Collision handling logic
    private void OnTriggerEnter2D(Collider2D point) 
    {
        if (!hasCollided) 
        {
            DetectPointType(point.GetComponent<Point>());
            hasCollided = true;
        }
    }

    private void OnTriggerExit2D(Collider2D point) 
    {
        hasCollided = false;
    }

    // Handle point type (rock, water, port, etc.)
    private void DetectPointType(Point point) 
    {
        switch (point.GetType()) 
        {
            case PointType.ROCK:
                TakeDamage(point.GetValue());
                break;
            case PointType.WATER:
                TakeWater(point.GetValue());
                break;
            case PointType.PORT:
                // Port logic
                Debug.Log("You're in the port.");
                break;
            case PointType.FIN:
                Debug.Log("You win!");
                break;
            default: 
                // Do nothing for other point types
                Debug.Log("Nothing happens here.");
                break;
        }
    }
}
