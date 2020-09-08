using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerSide currentTurnSide;


    public delegate void RunFinished(bool success, int serverType);
    public static event RunFinished OnRunFinished;

    [System.Serializable]
    public class Player
	{
        public Card_Identity identity;
        public Card[] deckCards;


    }

    [SerializeField]
    Player e_CorporationPlayer, e_RunnerPlayer;
    Player corporation, runner;



    // Start is called before the first frame update
    void Start()
    {
        SpawnAndSetCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void SpawnAndSetCards()
	{
        // Runner
        runner = new Player();
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





}
