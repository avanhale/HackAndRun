using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerSide currentTurnSide;


    public delegate void RunFinished(bool success, int serverType);
    public static event RunFinished OnRunFinished;

    public delegate void CardInstallation(Card card, bool installed);
    public static event CardInstallation OnCardInstallation;

    [System.Serializable]
    public class Player
	{
        public PlayerSide playerSide;
        public Card_Identity identity;
        public Card[] deckCards;

        public Player(PlayerSide _playerSide)
		{
            playerSide = _playerSide;
		}

        public bool IsRunner()
		{
            return playerSide == PlayerSide.Runner;
		}

    }

    [SerializeField]
    Player e_CorporationPlayer, e_RunnerPlayer;
    [HideInInspector]
    public Player corporation, runner;

	private void Awake()
	{
        instance = this;
	}

	// Start is called before the first frame update
	void Start()
    {
        SpawnAndSetCards();
        DrawFirstHands();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void SpawnAndSetCards()
	{
        // Runner
        runner = new Player(PlayerSide.Runner);
        Card_Identity playerIdentity = Instantiate(e_RunnerPlayer.identity);
        runner.identity = playerIdentity;

        int numCards = e_RunnerPlayer.deckCards.Length;
        runner.deckCards = new Card[numCards];
		for (int i = 0; i < numCards; i++)
		{
            Card card = Instantiate(e_RunnerPlayer.deckCards[i]);
            runner.deckCards[i] = card;
		}


        PlayArea.instance.SetCardsToSpots(runner);

	}


    void DrawFirstHands()
	{
        PlayCardManager.instance.DrawFirstHands();
	}

    
    void StartNextTurn()
	{
        PlayCardManager.instance.StartTurn(currentTurnSide == PlayerSide.Runner ? runner : corporation);
	}





}
