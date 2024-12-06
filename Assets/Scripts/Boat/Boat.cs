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
    public event Action<Player> OnBoatWin; 


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
        if (sunken){
            oldPositionIndex = 0;
            return;
        }
        
        var routeIndex = oldPositionIndex + cardValue;

        var points = route.GetPoints();
        if (routeIndex < 0) {
            routeIndex = 0;  
        }
        else if (routeIndex >= points.Length) {
            routeIndex = points.Length - 1;  
        }

        oldPositionIndex = routeIndex;
        transform.position = points[routeIndex].transform.position;
    }


    public bool buoyUsed = false;
    public void Buoy(Route route) {
        if(buoyUsed) { return;}
        buoyUsed = true;
        route.GetPoints()[oldPositionIndex].ThrowBuoy();
        int length = route.GetPoints().Length - oldPositionIndex; 
        Point[] newRoute = new Point[length];
        System.Array.Copy(route.GetPoints(), oldPositionIndex, newRoute, 0, length);
        oldPositionIndex = 0; 
        route.SetPoints(newRoute); 
    }

    // Reset buoy when we reset the game.
    public void ResetBuoy(Route route) {
        
        // reset buoy boolean
        buoyUsed = false;
        
        // return each point to it's original color.
        foreach(Point point in route.GetPoints()) {
            point.ResetSprite();
        }

        // restart route points to the originally assigned. 
        route.RestartPoints();
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
        Debug.Log("Stats have been restored. Integrity: " + Integrity + " Capacity: " + Capacity );
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
        OnIntegrityChanged?.Invoke(Integrity);
        if (Integrity <= 0) 
        { 
            sunken = true; 
        }
    }

    // Handle water taken by the boat
    public void TakeWater(int water) 
    {
        Capacity = Mathf.Clamp(Capacity - water, 0, maxCapacity);      
        OnCapacityChanged?.Invoke(Capacity);
        if (Capacity <= 0) 
        { 
            sunken = true; 
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
                AudioManager.Instance.PlaySound("Choque");
            //    AudioManager.Instance.PlaySound("Vida");
                break;
            case PointType.WATER:
                TakeWater(point.GetValue());
                AudioManager.Instance.PlaySound("Agua");
                break;
            case PointType.PORT:
                // Port logic
                Debug.Log("You're in the port.");
                break;
            case PointType.FIN:
                OnBoatWin?.Invoke(this.GetComponentInParent<Player>());
                break;
            default: 
                // Do nothing for other point types
                Debug.Log("Nothing happens here.");
                break;
        }
    }
}
