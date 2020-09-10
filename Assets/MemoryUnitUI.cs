﻿using UnityEngine;
using UnityEngine.UI;

public class MemoryUnitUI : MonoBehaviour
{
    public PlayerSide playerSide;
    PlayerNR myPlayer;
    Button[] memoryUnitButtons;

	private void Awake()
	{
        myPlayer = playerSide == PlayerSide.Runner ? PlayerNR.Runner : PlayerNR.Corporation;
        GetMemoryUnitButtons();
    }

    private void OnEnable()
    {
        myPlayer.OnMemoryUnitsTotalChanged += MyPlayer_OnMemoryUnitsTotalChanged;
		PlayCardManager.OnCardInstalled += PlayCardManager_OnCardInstalled;
    }

	private void OnDisable()
    {
        myPlayer.OnMemoryUnitsTotalChanged -= MyPlayer_OnMemoryUnitsTotalChanged;
		PlayCardManager.OnCardInstalled -= PlayCardManager_OnCardInstalled;
    }

    void MyPlayer_OnMemoryUnitsTotalChanged()
	{
        UpdateMemoryUnitButtons();
	}

    private void PlayCardManager_OnCardInstalled(Card card, bool success)
    {
        UpdateMemoryUnitButtons();
    }

    void UpdateMemoryUnitButtons()
    {
        // Activate and set Interactive
        for (int i = 0; i < memoryUnitButtons.Length; i++)
        {
            memoryUnitButtons[i].interactable = false;
            memoryUnitButtons[i].gameObject.SetActive(false);
        }

        int memoryAvailable = myPlayer.MemoryAvailable();
        for (int i = 0; i < myPlayer.MemoryUnitsTotal; i++)
        {
            int targetIndex = memoryUnitButtons.Length - (i + 1);
            if (targetIndex >= 0 && targetIndex < memoryUnitButtons.Length)
			{
                bool memoryUsed = (i + 1) > memoryAvailable;
                memoryUnitButtons[targetIndex].interactable = !memoryUsed;
                memoryUnitButtons[targetIndex].gameObject.SetActive(true);
            }
        }
    }


    void GetMemoryUnitButtons()
    {
        memoryUnitButtons = GetComponentsInChildren<Button>(true);
    }


}