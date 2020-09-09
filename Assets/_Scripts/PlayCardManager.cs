using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCardManager : MonoBehaviour
{
    public static PlayCardManager instance;
    public int numCardsToDrawFirstHand;


	private void Awake()
	{
        instance = this;
    }

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DrawFirstHands()
	{
        Card[] runnerCards = new Card[numCardsToDrawFirstHand];
		for (int i = 0; i < numCardsToDrawFirstHand; i++)
		{
            Card drawnCard = PlayArea.instance.DrawCardFromDeck(GameManager.instance.runner);
            runnerCards[i] = drawnCard;
		}

        PlayArea.instance.AddCardsToHand(GameManager.instance.runner, runnerCards);


	}


    public void StartTurn(GameManager.Player playerTurn)
	{
        PlayArea.instance.ResetActionTracker(playerTurn);
	}





}
