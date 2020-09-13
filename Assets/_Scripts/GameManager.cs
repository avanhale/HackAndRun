using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerSide currentTurnSide;
    public static PlayerNR CurrentTurnPlayer;


    public delegate void RunFinished(bool success, int serverType);
    public static event RunFinished OnRunFinished;

    [Header("Game Data")]
    public int numCardsToDrawFirstHand;
    public int numCreditsToStartWith;


    private void Awake()
	{
        instance = this;
        UpdateCurrentTurn(currentTurnSide);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
	{
        yield return new WaitForSeconds(0.25f);

        SpawnAndSetCards(PlayerNR.Runner);
        SpawnAndSetCards(PlayerNR.Corporation);

        yield return new WaitForSeconds(0.25f);

        SetPlayerCredits(PlayerNR.Runner, numCreditsToStartWith);
        SetPlayerCredits(PlayerNR.Corporation, numCreditsToStartWith);
        DrawFirstHands();
        StartNextTurn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    void SpawnAndSetCards(PlayerNR player)
	{
        // Runner
        Card_Identity identityCard = Instantiate(player.e_PlayerDeck.identityCard);

        Card[] e_deckCards = player.e_PlayerDeck.deckCards;
        int numCards = e_deckCards.Length;
        Card[] deckCards = new Card[numCards];
        for (int i = 0; i < numCards; i++)
		{
            Card card = Instantiate(e_deckCards[i]);
            deckCards[i] = card;

        }

        player.SetPlayerCards(identityCard, deckCards);
        PlayArea.instance.SetCardsToSpots(player);

	}

    void SetPlayerCredits(PlayerNR player, int numCredits)
	{
        player.Credits = numCredits;
	}


    void DrawFirstHands()
	{
        PlayCardManager.instance.DrawCards(PlayerNR.Corporation, numCardsToDrawFirstHand);
        PlayCardManager.instance.DrawCards(PlayerNR.Runner, numCardsToDrawFirstHand);
    }


    void StartNextTurn()
	{
        UpdateCurrentTurn(currentTurnSide);
        PlayCardManager.instance.StartTurn(CurrentTurnPlayer);
	}

    void UpdateCurrentTurn(PlayerSide side)
	{
        currentTurnSide = side;
        CurrentTurnPlayer = side == PlayerSide.Runner ? PlayerNR.Runner : PlayerNR.Corporation;
    }


    public bool IsCurrentTurn(PlayerSide playerSide)
	{
        return currentTurnSide == playerSide;
	}



}
