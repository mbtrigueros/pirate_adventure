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

    [SerializeField] Image[] avatars;

    [SerializeField] Sprite[] avatar1Sprites;
    [SerializeField] Sprite[] avatar2Sprites;
    [SerializeField] TextMeshProUGUI[] integrityTexts, capacityTexts, playerTitles;

    [SerializeField] Color lorenzoColor;
    [SerializeField] Color carpiColor;

    [SerializeField] GameObject winMenu;
    [SerializeField] TextMeshProUGUI winnerName;
    [SerializeField] Image winnerAvatar;

    [SerializeField] Canvas overlay;

    // New Image components for integrity and capacity
    [SerializeField] Image[] integrityImage, capacityImage; 
    [SerializeField] Sprite[] integritySprites; // Array of sprites for integrity levels (sliced from the sprite sheet)
    [SerializeField] Sprite[] capacitySprites; // Array of sprites for capacity levels (sliced from the sprite sheet)

    void Start()
    {
        if (players != null)
        {
            for (int i = 0; i < players.Length; i++)
            {
                var boat = players[i].GetPlayerBoat();
                int playerIndex = i;
                boat.OnIntegrityChanged += (currentIntegrity) => UpdateBoatUI(playerIndex, currentIntegrity, boat.GetMaxIntegrity(), true);
                boat.OnCapacityChanged += (currentCapacity) => UpdateBoatUI(playerIndex, currentCapacity, boat.GetMaxCapacity(), false);
                TurnController.Instance.OnTurnChanged += HandleTurnChanged;
                boat.OnBoatWin += HandleBoatWin;

            }
            HandleTurnChanged(players[0]);

            InitializeHUD();
        }
    }

    private void HandleBoatWin(Player currentPlayer)
    {
        winMenu.SetActive(true);
        overlay.gameObject.SetActive(true);
        Time.timeScale = 0;
            for (int i = 0; i < players.Length; i++)   {
                if (players[i] == currentPlayer) {
                    winnerName.text = "El ganador es: " + playerTitles[i].text + "!!!";
                    winnerAvatar.sprite = i == 0 ? avatar1Sprites[3] : avatar2Sprites[3];
                } 
        }
    }

    private void HandleTurnChanged(Player currentPlayer)
    {
        for (int i = 0; i < players.Length; i++)
        {
            currentPlayerTurnText.GetComponent<Animator>().Play("fade_out");
            if (players[i] == currentPlayer)
            {
                SetCurrentPlayerAvatarAnimation(i);
                SetCurrentPlayerText(i);
            }
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
        for (int i = 0; i < players.Length; i++)
        {
            SetPlayerTitle(i);

            var boat = players[i].GetPlayerBoat();
            if (boat != null)
            {
                // Update the UI with the initial values for integrity and capacity
                UpdateBoatUI(i, players[i].GetPlayerBoat().Integrity, boat.GetMaxIntegrity(), true);
                UpdateBoatUI(i, players[i].GetPlayerBoat().Capacity, boat.GetMaxCapacity(), false);
            }
        }
    }

    void SetPlayerTitle(int playerIndex)
    {
        playerTitles[playerIndex].text = playerIndex == 0 ? "Lorenzo" : "Carpi";
    }

    void SetCurrentPlayerText(int playerIndex)
    {
        currentPlayerTurnText.text = playerIndex == 0 ? "Turno de Lorenzo" : "Turno de Carpi";
        currentPlayerTurnText.color = playerIndex == 0 ? lorenzoColor : carpiColor;
    }

    void SetCurrentPlayerAvatarAnimation(int playerIndex)
    {
        if (playerIndex == 0)
        {
            var animatorCurrent = avatars[0].GetComponent<Animator>();
            animatorCurrent.Rebind();
            animatorCurrent.Play("rotate");
            var animatorOther = avatars[1].GetComponent<Animator>();
            animatorOther.Rebind();
            animatorOther.Play("turn_gray");
        }
        else
        {
            var animatorCurrent = avatars[1].GetComponent<Animator>();
            animatorCurrent.Rebind();
            animatorCurrent.Play("rotate");
            var animatorOther = avatars[0].GetComponent<Animator>();
            animatorOther.Rebind();
            animatorOther.Play("turn_gray");
        }
    }

    void UpdateBoatUI(int playerIndex, int currentValue, int maxValue, bool isIntegrity)
    {
        // Update the sprite based on the stat value (integrity or capacity)
        if (isIntegrity)
        {
            UpdateIntegrityImage(playerIndex, currentValue, maxValue);
            UpdateAvatarSprite(playerIndex, currentValue);  // Update avatar when integrity changes
        }
        else
        {
            UpdateCapacityImage(playerIndex, currentValue, maxValue);
        }
    }

    void UpdateIntegrityImage(int playerIndex, int currentIntegrity, int maxValue)
    {
        // Ensure the integrity value stays within the bounds of the sprite array
        int spriteIndex = Mathf.Clamp(currentIntegrity, 0, integritySprites.Length - 1);

        // Update the sprite based on the current integrity value
        integrityImage[playerIndex].sprite = integritySprites[spriteIndex];
        avatars[playerIndex].sprite = integritySprites[spriteIndex];
        integrityTexts[playerIndex].text = $"{currentIntegrity} / {maxValue}";
    }

    void UpdateCapacityImage(int playerIndex, int currentCapacity, int maxValue)
    {
        // Ensure the capacity value stays within the bounds of the sprite array
        int spriteIndex = Mathf.Clamp(currentCapacity, 0, capacitySprites.Length - 1);

        // Update the sprite based on the current capacity value
        capacityImage[playerIndex].sprite = capacitySprites[spriteIndex];
        capacityTexts[playerIndex].text = $"{currentCapacity} / {maxValue}";
    }

    void UpdateAvatarSprite(int playerIndex, int currentIntegrity)
    {
        // Map the current integrity to the appropriate sprite index using math
        int spriteIndex = Mathf.FloorToInt((float)currentIntegrity / 2);  // Divide by 2 since we have 4 sprites for 8 points

        // Ensure the sprite index is within bounds
        spriteIndex = Mathf.Clamp(spriteIndex, 0, avatar1Sprites.Length - 1);

        // Update the avatar sprite for the player
        avatars[playerIndex].sprite = playerIndex == 0 ? avatar1Sprites[spriteIndex] : avatar2Sprites[spriteIndex];
    }

}
