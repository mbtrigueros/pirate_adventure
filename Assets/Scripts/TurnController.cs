using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Esta clase es el controlador de turnos. Funciona medio como un Game Manager, en el sentido en que tmb es un Singleton (es decir que tiene una única instancia y no se elimina a lo largo del juego). Acá, obviamente, vamos a estar controlando el sistema de turnos de los jugadores.
public class TurnController : MonoBehaviour
{
    // Delegate TurnChanged que toma como parámetro el jugador actual.  
    public delegate void TurnChanged(Player currentPlayer);

    // Evento de tipo TurnChanged que se llama cuando cambió el turno. 
    public event TurnChanged OnTurnChanged;

    // Acá establecemos la instancia.
    public static TurnController Instance { get; private set; }
    
    // Referencia al array de jugadores que aplicaremos desde el editor.
    [SerializeField] Player[] players;

    // Controlamos con esta variable el índice del jugador.
    private int currentPlayerIndex = 0;
    
    // El método Awake es un método que nos provee la clase MonoBehaviour de Unity, que es llamado ni bien inicializamos el juego.
    // Acá lo que estamos estableciendo es la instancia del TurnController y aclaramos que no lo destruya al cambiar de escena. 
    // Si ya existe una instancia, no crea otra. Es única.
    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else {
            Destroy(gameObject);
        }
    }

    // En el método Start llamamos al método StartPlayerTurn.
    private void Start() {
        StartPlayerTurn();
    }

    // Método para iniciar el turno.
    private void StartPlayerTurn() {

        // Igualamos el jugador actual al indice del array de jugadores.
        Player currentPlayer = CurrentPlayer();
        OnTurnChanged?.Invoke(currentPlayer); 
    }

    // Método para cambiar de turno. 
    public void SwitchTurn() {
        // Igualamos el indice del jugador + 1 y hacemos el módulo del largo del array. Esto nos va a devolver 0 o 1, cambiando así el turno. 
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Length; 
        // Luego iniciamos el turno.
        StartPlayerTurn(); 
    }

    // Devolvemos el indice del jugador actual.
    public Player CurrentPlayer() {
        return players[currentPlayerIndex];
    }

    // Chequeamos que el jugador sea el actual.
    public bool IsCurrentPlayer(Player player) {
        return player == players[currentPlayerIndex]; 
    }
}
