using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{

    private Cards boatDeck;
    public int Integrity { get; private set; }
    public int Capacity { get; private set; }

    [SerializeField] private int integrity, capacity;

    [SerializeField] private int anchoring; // estado de fondeo.

    [SerializeField] private BoatType boatType;


    [SerializeField] public float speed = 5f;
    [SerializeField] private bool sunken = false;

    private bool isMoving;

    // Start is called before the first frame update

    private void Awake() {
        boatDeck = GameObject.Find("DeckManager").GetComponent<Cards>();   
    }
    void Start()
    {
        Integrity = integrity;
        Capacity = capacity;

        isMoving = false;

        GenerateBoatDeck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    int oldPositionIndex = 0;
    public IEnumerator Move(Route route, int cardValue) {

        isMoving = true;
        
        var routeIndex = oldPositionIndex + cardValue;  
        while (Vector3.Distance(transform.position, route.GetPoints()[routeIndex].transform.position) > 0.01f) 
        {
            transform.position = Vector3.MoveTowards(transform.position, route.GetPoints()[routeIndex].transform.position, speed * Time.deltaTime);
            oldPositionIndex = routeIndex;
            yield return null;
        }
        
        transform.position = route.GetPoints()[routeIndex].transform.position;
        isMoving = false;  
        Debug.Log("Reached position. Is Moving? = " + isMoving);
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
    }

    public Cards GetBoatDeck() {
        return boatDeck;
    }

    private bool hasCollided = false;
    private void OnTriggerStay2D(Collider2D point) {
        if(!hasCollided && !isMoving) {
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
            // damage boat
            TakeDamage(point.GetValue());
            break;
            case PointType.WATER:
            // fill boat with water
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
