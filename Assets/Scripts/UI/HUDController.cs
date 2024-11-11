using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] List<Player> players = new List<Player>();

    [SerializeField] List<TextMeshProUGUI> integrityTexts = new List<TextMeshProUGUI>();
    [SerializeField] List<TextMeshProUGUI> capacityTexts = new List<TextMeshProUGUI>();
    [SerializeField] List<TextMeshProUGUI> playerTitles = new List<TextMeshProUGUI>();

    void Start()
    {
        // Subscribe to the boat events for each player
        for (int i = 0; i < players.Count; i++)
        {
            var boat = players[i].GetPlayerBoat();

            // Subscribe to events
            boat.OnIntegrityChanged += (currentIntegrity) => UpdateBoatUI(i, currentIntegrity, true);
            boat.OnCapacityChanged += (currentCapacity) => UpdateBoatUI(i, currentCapacity, false);
        }

        // Initialize the HUD with the current values
        InitializeHUD();
    }

    // Unsubscribe from events when the HUDController is destroyed
    private void OnDestroy()
    {
        for (int i = 0; i < players.Count; i++)
        {
            var boat = players[i].GetPlayerBoat();
            boat.OnIntegrityChanged -= (currentIntegrity) => UpdateBoatUI(i, currentIntegrity, true);
            boat.OnCapacityChanged -= (currentCapacity) => UpdateBoatUI(i, currentCapacity, false);
        }
    }

    // Initialize the HUD with the current values for each boat
    void InitializeHUD()
    {
        for (int i = 0; i < players.Count; i++)
        {
            SetPlayerTitle(i);

            UpdateBoatUI(i, players[i].GetPlayerBoat().Integrity, true);
            UpdateBoatUI(i, players[i].GetPlayerBoat().Capacity, false);
        }
    }

    void SetPlayerTitle(int playerIndex)
    {
        if (playerTitles != null && playerIndex < playerTitles.Count)
        {
            playerTitles[playerIndex].text = $"Jugador {playerIndex}";
        }
    }

    // Method to update the UI for a specific player (boat)
    void UpdateBoatUI(int playerIndex, int currentValue, bool isIntegrity)
    {
        if (isIntegrity)
        {
            // Update integrity slider and text
            integrityTexts[playerIndex].text = $"{currentValue}";
        }
        else
        {
            // Update capacity slider and text
            capacityTexts[playerIndex].text = $"{currentValue}";
        }
    }
}
