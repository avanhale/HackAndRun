using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerSide currentTurnSide;


    public delegate void RunFinished(bool success, int serverType);
    public static event RunFinished OnRunFinished;

    [Header("Game Data")]
    public int numCardsToDrawFirstHand;
    public int numCreditsToStartWith;


    private void Awake()
	{
        instance = this;
	}

	// Start is called before the first frame update
	void Start()
    {
        SpawnAndSetCards();
        SetPlayerCredits(PlayerNR.Runner, numCreditsToStartWith);
        DrawFirstHands();
        StartNextTurn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    void SpawnAndSetCards()
	{
        // Runner
        Card_Identity runnerIdentity = Instantiate(PlayerNR.Runner.e_PlayerDeck.identityCard);

        Card[] e_deckCards = PlayerNR.Runner.e_PlayerDeck.deckCards;
        int numCards = e_deckCards.Length;
        Card[] deckCards = new Card[numCards];
		for (int i = 0; i < numCards; i++)
		{
            Card card = Instantiate(e_deckCards[i]);
            deckCards[i] = card;
		}

        PlayerNR.Runner.SetPlayerCards(runnerIdentity, deckCards);
        PlayArea.instance.SetCardsToSpots(PlayerNR.Runner);

	}

    void SetPlayerCredits(PlayerNR player, int numCredits)
	{
        player.Credits = numCredits;
	}


    void DrawFirstHands()
	{
        PlayCardManager.instance.DrawCards(numCardsToDrawFirstHand);
	}

    
    void StartNextTurn()
	{
        PlayCardManager.instance.StartTurn(currentTurnSide == PlayerSide.Runner ? PlayerNR.Runner : PlayerNR.Corporation);
	}





}
