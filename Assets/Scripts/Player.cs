using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static List<Player> players = new();
    [SerializeField] private float speed = 5f;
    private Vector3 target;

    private bool moved = false;

    // Start is called before the first frame update
    void Start()
    {
        players.Add(this);
        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !moved && players.IndexOf(this) == 0) {
            target = Camera.main.ScreenToWorldPoint( Input.mousePosition );
            target.z = transform.position.z;
            moved = true;
        }

            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            if(transform.position == target && moved && players.IndexOf(this) == 0)
            { 
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;

                foreach(Player player in players) {
                    if (player != this) {
                        player.moved = false;
                        player.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                    }
                }
                players.Reverse();
            }
    }

    private void OnTriggerEnter2D(Collider2D other) {
            print("Touched Point");
        
    }
    
}
