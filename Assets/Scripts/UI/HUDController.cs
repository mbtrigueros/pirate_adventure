using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] Player[] players;
    [SerializeField] TextMeshProUGUI currentPlayerTurnText;

    [SerializeField] TextMeshProUGUI[] integrityTexts, capacityTexts, playerTitles;

    void Start()
    {
        if (players != null) {

                for (int i = 0; i < players.Length; i++)
                {
                    var boat = players[i].GetPlayerBoat();
                    int playerIndex = i; 
                    boat.OnIntegrityChanged += (currentIntegrity) => UpdateBoatUI(playerIndex, currentIntegrity, boat.GetMaxIntegrity(), true);
                    boat.OnCapacityChanged += (currentCapacity) => UpdateBoatUI(playerIndex, currentCapacity, boat.GetMaxCapacity(), false);
                    TurnController.Instance.OnTurnChanged += HandleTurnChanged;
                }
                HandleTurnChanged(players[0]);

                InitializeHUD();
            
        }
    }

    private void HandleTurnChanged(Player currentPlayer)
    {
        for (int i = 0; i < players.Length; i++) {
            currentPlayerTurnText.GetComponent<Animator>().Play("fade_out");
            if (players[i] == currentPlayer) SetCurrentPlayerText(i);
        }
    }

    // Unsubscribe from events when the HUDController is destroyed
    private void OnDestroy()
    {
        for (int i = 0; i < players.Length; i++)
        {
            var boat = players[i].GetPlayerBoat();
            int playerIndex = i; 
            boat.OnIntegrityChanged -= (currentIntegrity) => UpdateBoatUI(playerIndex, currentIntegrity, boat.GetMaxIntegrity(), true);
            boat.OnCapacityChanged -= (currentCapacity) => UpdateBoatUI(playerIndex, currentCapacity, boat.GetMaxCapacity(), false);
        }
    }

    // Initialize the HUD with the current values for each boat
    void InitializeHUD()
    {
        for (int i = 0; i < players.Length; i++) {
                SetPlayerTitle(i);

            var boat = players[i].GetPlayerBoat();
            if (boat != null) {
                UpdateBoatUI(i, players[i].GetPlayerBoat().Integrity, boat.GetMaxIntegrity(), true);
                UpdateBoatUI(i, players[i].GetPlayerBoat().Capacity, boat.GetMaxCapacity(), false);
            }
        }
    }

    void SetPlayerTitle(int playerIndex)
    {
        playerTitles[playerIndex].text = playerIndex == 0 ? "Lorenzo" : "Carpi";
        
    }

    void SetCurrentPlayerText(int playerIndex) {
        currentPlayerTurnText.text = playerIndex == 0 ? "Turno de Lorenzo" : "Turno de Carpi";
    }

    void UpdateBoatUI(int playerIndex, int currentValue, int maxValue, bool isIntegrity) {
        // Debug the index and array lengths
        Debug.Log($"Updating UI for player {playerIndex}. Array lengths: " +
                $"integrityTexts: {integrityTexts.Length}, " +
                $"capacityTexts: {capacityTexts.Length}, " +
                $"playerTitles: {playerTitles.Length}");
                

        if (isIntegrity) {
                integrityTexts[playerIndex].text = $"HP: {currentValue} / {maxValue}";
        }
        else {
                capacityTexts[playerIndex].text = $"CAP: {currentValue} / {maxValue}";
        }

    }
}
