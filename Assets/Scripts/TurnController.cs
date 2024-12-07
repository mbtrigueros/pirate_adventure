using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TurnController : MonoBehaviour
{
    public delegate void TurnChanged(Player currentPlayer);

    public event TurnChanged OnTurnChanged;

    public static TurnController Instance { get; private set; }
    

    [SerializeField] Player[] players;


    private int currentPlayerIndex = 0;
    

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            Debug.Log("TurnController instatiated");
            DontDestroyOnLoad(gameObject); 
        }
        else {
            Destroy(gameObject);
        }
    }

    // En el método Start llamamos al método StartPlayerTurn.
    private void Start() {
        StartPlayerTurn();
        foreach (Player player in players) {
            Debug.Log($"{player.name} is subscribed: {player.suscribed}");
        }
    }

    public void StartPlayerTurn() {
        Player currentPlayer = CurrentPlayer();
        Debug.Log("Starting turn for player: " + currentPlayer.name);
        OnTurnChanged?.Invoke(currentPlayer);
    }


    public void SwitchTurn() {

        players[currentPlayerIndex].ClearDrawnCards();
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Length; 
        Debug.Log("Current player index is now: " + currentPlayerIndex);

        StartPlayerTurn(); 
        Debug.Log("Switched turn to: " + CurrentPlayer().name);
    }

    public IEnumerator SwitchTurnCoroutine() {
        yield return new WaitForSeconds(.5f);  
        SwitchTurn();
    }

    public Player CurrentPlayer() {
        return players[currentPlayerIndex];
    }

    public bool IsCurrentPlayer(Player player) {
    bool isCurrent = player == players[currentPlayerIndex];
    Debug.Log($"Is {player.name} current player? {isCurrent}");     
    return isCurrent;
    }

    public void RestartGame() {
        Time.timeScale = 1;

        foreach (Player player in players) {
            player.GetPlayerBoat().ResetBuoy(player.GetPlayerRoute());
            player.GetPlayerBoat().ResetToPort(player.GetPlayerRoute());
            player.GetPlayerBoat().GenerateBoatDeck();
        }
    }
}
