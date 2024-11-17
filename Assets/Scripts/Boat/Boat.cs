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

    [SerializeField] private int anchoring; // estado de fondeo.
    [SerializeField] private BoatType boatType;

    [SerializeField] public float speed = 5f;
    [SerializeField] private bool sunken = false;

    public event Action<int> OnIntegrityChanged;
    public event Action<int> OnCapacityChanged; 

    public event Action OnBoatSunk; 

    // Start is called before the first frame update
    private void Awake() 
    {
        // Initialize the boat deck
        if (boatDeck != null) {
            boatDeck.GenerateDeck(boatType);
            Debug.Log("Deck Generated: " + boatDeck.GetDeckType() + " Kind of Boat I am: " + this.boatType);
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
        var routeIndex = oldPositionIndex + cardValue + 1;   
        oldPositionIndex = routeIndex;
        transform.position = route.GetPoints()[routeIndex].transform.position;
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

    private void Anchor() 
    {
        // Implement anchoring logic here
    }

    private void CheckAnchoring()
    {
        // Check if the boat is anchored and update state accordingly
    }

    public void ResetToLastPort(Route route)
    {
            var section = route.GetSection();
            var port = section.GetPorts()[0];
            transform.position = port.transform.position;
            Debug.Log("The boat has been reset to the starting position.");
        
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
        Debug.Log("You hit a rock and took " + damage + " damage. Your integrity is now: " + Integrity);
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
            boatDeck.GenerateDeck(boatType);
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
            default: 
                // Do nothing for other point types
                Debug.Log("Nothing happens here.");
                break;
        }
    }
}
