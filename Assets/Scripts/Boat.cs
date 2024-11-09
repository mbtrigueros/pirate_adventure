using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{

    [SerializeField] private Cards boatDeck;
    public int Integrity { get; private set; }
    public int Capacity { get; private set; }

    [SerializeField] private int integrity, capacity;

    [SerializeField] private int anchoring; // estado de fondeo.

    [SerializeField] private BoatType boatType;


    [SerializeField] public float speed = 5f;
    [SerializeField] private bool sunken = false;

    // Start is called before the first frame update

    private void Awake() {
        boatDeck.GenerateDeck(boatType);
        Debug.Log("Deck Generated: " + boatDeck.GetDeck() + "Kind of Boat I am: " + this.boatType);
    }
    void Start()
    {
        Integrity = integrity;
        Capacity = capacity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    int oldPositionIndex = 0;
    public void Move(Route route, int cardValue) {

        var routeIndex = oldPositionIndex + cardValue + 1;  
        oldPositionIndex = routeIndex;
        
        transform.position = route.GetPoints()[routeIndex].transform.position;
    }

    public void Repair(int health){
        Integrity += health;
    }

    public void Empty(int water){
        Capacity += water;
    }

    private void Anchor(){

    }

    private void CheckAnchoring(){

    }

    public void TakeDamage(int damage) {
        Integrity -= damage;
        Debug.Log("Golpeaste una roca que te lastimo esta cantidad: " + damage + ". Tu integridad ahora es de :" + Integrity);
        if (Integrity <= 0 ) { sunken = true; }
    }

    public void TakeWater(int water) {
        Capacity -= water;
        Debug.Log("Te llenaste de " + water + " cantidad de agua. Tu capacidad ahora es de: " + Capacity);
        if (Capacity <= 0 ) { sunken = true; }
    }

    public void GenerateBoatDeck() {
        if (boatDeck) boatDeck.GenerateDeck(boatType);
        Debug.Log("Generating deck...");
    }

    public Cards GetBoatDeck() {
        return boatDeck;
    }

    private bool hasCollided = false;
    private void OnTriggerStay2D(Collider2D point) {
        if(!hasCollided) {
            DetectPointType(point.GetComponent<Point>());
            hasCollided = true;
        }
    }

    private void OnTriggerExit2D(Collider2D point) {
        hasCollided = false;
    }
    private void DetectPointType(Point point) {
        switch(point.GetType()) {
            case PointType.ROCK:
            TakeDamage(point.GetValue());
            break;
            case PointType.WATER:
            TakeWater(point.GetValue());
            break;
            case PointType.PORT:
            // port logic
            Debug.Log("Estás en el puerto.");
            break;
            default: 
            // do nothing
            Debug.Log("Acá no pasa nada :)");
            break;
        }
    }

}
