using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public delegate void TurnChanged(Player currentPlayer);
    public event TurnChanged OnTurnChanged;
    public static TurnController Instance { get; private set; }
    [SerializeField] Player[] players;
    private int currentPlayerIndex = 0;
    
    
    private void Awake() {
                if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartPlayerTurn();
    }

    private void StartPlayerTurn()
    {
        Player currentPlayer = CurrentPlayer();
        Debug.Log("It's now " + currentPlayer.name + "'s turn.");
        OnTurnChanged?.Invoke(currentPlayer); // Notify subscribers of the turn change
    }

    public void SwitchTurn()
    {
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Length; // Switch turn
        StartPlayerTurn(); // Start the new player's turn
    }

    public Player CurrentPlayer()
    {
        return players[currentPlayerIndex]; // Return the currently active player
    }

    public bool IsCurrentPlayer(Player player)
    {
        return player == players[currentPlayerIndex]; // Check if the player is current
    }
}
